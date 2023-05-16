using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, AuthResponseDTO>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;

        public UserRegistrationCommandHandler(UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponseDTO> Handle(UserRegistrationCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userManager.FindByEmailAsync(request.Email);
            var phoneExists = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber.Equals(request.PhoneNumber));

            if (emailExists != null || phoneExists != null) throw new EmailOrPhoneTakenException("Email and Phone already taken");
            
            var user = new User()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Type = UserType.Seller,
                Logo = "path"
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new InvalidOperationException("User creation failed");

            var refreshToken = await _tokenService.GenerateRefreshToken();
            var accessToken = await _tokenService.GenerateJwtToken(user);

            await _refreshTokenRepository.CreateAsync(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(30), user.Id));

            var authDTO = new AuthResponseDTO { Type = UserType.Seller, UserId = user.Id, AccessToken = accessToken, RefreshToken = refreshToken };
            return new AuthResponseDTO();
        }
    }
}
