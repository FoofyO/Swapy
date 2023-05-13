using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System.Security.Authentication;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthResponseDTO>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponseDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrPhone);

            if (user == null)
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.EmailOrPhone);
                if (user == null) throw new NotFoundException("Invalid email or password");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new AuthenticationException("Invalid email or password");

            var refreshToken = await _tokenService.GenerateRefreshToken();
            var accessToken = await _tokenService.GenerateJwtToken(user);
            await _refreshTokenRepository.CreateAsync(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(30), user.Id));

            var authDTO = new AuthResponseDTO { UserId = user.Id, AccessToken = accessToken, RefreshToken = refreshToken };
            return authDTO;
        }
    }
}
