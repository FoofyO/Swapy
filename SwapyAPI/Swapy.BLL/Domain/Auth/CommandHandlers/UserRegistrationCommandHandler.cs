using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Auth.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class UserRegistrationCommandHandler : IRequestHandler<UserRegistrationCommand, AuthResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserTokenRepository _userTokenRepository;

        public UserRegistrationCommandHandler(UserManager<User> userManager, IUserTokenRepository userTokenRepository, IUserTokenService userTokenService)
        {
            _userManager = userManager;
            _userTokenService = userTokenService;
            _userTokenRepository = userTokenRepository;
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
                Logo = "path",
            };
            
            var refreshToken = await _userTokenService.GenerateRefreshToken();
            var accessToken = await _userTokenService.GenerateJwtToken(user.Id, user.Email, user.FirstName, user.LastName);
            
            user.UserName = user.Id.Replace("-", "");
            
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new InvalidOperationException("User creation failed");
            
            user.UserTokenId = refreshToken;
            await _userTokenRepository.CreateAsync(new UserToken(accessToken, refreshToken, DateTime.UtcNow.AddDays(30), user.Id));

            return new AuthResponseDTO { Type = UserType.Seller, UserId = user.Id, AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
