using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, AuthResponseDTO>
    {
        private readonly string _userId;
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public RefreshTokenCommandHandler(UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponseDTO> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            string accessToken = request.OldAccessToken;
            string refreshToken = request.OldRefreshToken;
            
            var expirationTime = new JwtSecurityToken(request.OldAccessToken).ValidTo;
            
            if (expirationTime < DateTime.Now)
            {
                var token = await _refreshTokenRepository.GetByIdAsync(request.OldRefreshToken);

                await _refreshTokenRepository.DeleteAsync(token);

                if (token.ExpiresAt < DateTime.Now) throw new TokenExpiredException("The provided refresh token has expired");

                var user = await _userManager.FindByIdAsync(_userId);

                accessToken = await _tokenService.GenerateJwtToken(user);
                refreshToken = await _tokenService.GenerateRefreshToken();
                await _refreshTokenRepository.CreateAsync(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(30), user.Id));
            }

            var authDTO = new AuthResponseDTO { UserId = _userId, AccessToken = accessToken, RefreshToken = refreshToken };
            return authDTO;
        }
    }
}
