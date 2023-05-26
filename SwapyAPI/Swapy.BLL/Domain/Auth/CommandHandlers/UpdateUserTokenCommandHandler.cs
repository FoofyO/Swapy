using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Auth.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using System.IdentityModel.Tokens.Jwt;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class UpdateUserTokenCommandHandler : IRequestHandler<UpdateUserTokenCommand, AuthResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserTokenRepository _refreshTokenRepository;

        public UpdateUserTokenCommandHandler(UserManager<User> userManager, IUserTokenRepository refreshTokenRepository, IUserTokenService userTokenService)
        {
            _userManager = userManager;
            _userTokenService = userTokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponseDTO> Handle(UpdateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var user = new User();
            var userToken = new UserToken();
            var accessToken = request.OldAccessToken;
            var refreshToken = request.OldRefreshToken;

            try
            {
                userToken = await _refreshTokenRepository.GetByIdAsync(refreshToken);
            }
            catch (NotFoundException)
            {
                throw new TokenExpiredException("The provided Refresh token has expired");
            }

            if (!accessToken.Equals(userToken)) throw new TokenExpiredException("The provided Access token has expired");

            var expirationTime = new JwtSecurityToken(userToken.AccessToken).ValidTo;

            if (expirationTime < DateTime.Now)
            {
                user = await _userManager.FindByIdAsync(request.UserId);
                await _refreshTokenRepository.DeleteAsync(userToken);

                if (userToken.ExpiresAt < DateTime.Now) throw new TokenExpiredException("The provided Refresh token has expired");


                accessToken = await _userTokenService.GenerateJwtToken(user.Id, user.Email, user.FirstName, user.LastName);
                refreshToken = await _userTokenService.GenerateRefreshToken();
                
                user.UserTokenId = refreshToken;
                await _refreshTokenRepository.CreateAsync(new UserToken(accessToken, refreshToken, DateTime.UtcNow.AddDays(30), user.Id));
            }

            var authDTO = new AuthResponseDTO { UserId = user.Id, Type = user.Type, AccessToken = accessToken, RefreshToken = refreshToken };
            return authDTO;
        }
    }
}
