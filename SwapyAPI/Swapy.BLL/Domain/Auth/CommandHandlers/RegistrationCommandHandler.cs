using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, AuthResponseDTO>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly RefreshTokenRepository _refreshTokenRepository;

        public RegistrationCommandHandler(UserManager<User> userManager, RefreshTokenRepository refreshTokenRepository, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
        }

        public async Task<AuthResponseDTO> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userManager.FindByEmailAsync(request.Email);
            var phoneExists = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.Phone);

            if (emailExists != null && phoneExists == null) throw new EmailOrPhoneTakenException("Email already taken");
            else if (emailExists == null && phoneExists != null) throw new EmailOrPhoneTakenException("Phone already taken");
            else if (emailExists != null && phoneExists != null) throw new EmailOrPhoneTakenException("Email and Phone already taken");
            else
            {
                //var user = new User(request.FullName, request.Email, request.Phone, string.Empty);
                //var result = await _userManager.CreateAsync(user, request.Password);

                //if (!result.Succeeded) throw new InvalidOperationException("User creation failed");

                //var refreshToken = await _tokenService.GenerateRefreshToken();
                //var accessToken = await _tokenService.GenerateJwtToken(user);
                //await _refreshTokenRepository.CreateAsync(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(30), user.Id));

                //var authDTO = new AuthResponseDTO { SellerId = user.Id, Email = user.Email, Phone = user.PhoneNumber, AccessToken = accessToken, RefreshToken = refreshToken };
                return new AuthResponseDTO();
            }
        }
    }
}