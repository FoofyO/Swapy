using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.DTO.Auth.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class ShopRegistrationCommandHandler : IRequestHandler<ShopRegistrationCommand, AuthResponseDTO>
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public ShopRegistrationCommandHandler(IEmailService emailService, UserManager<User> userManager, IUserTokenRepository userTokenRepository, IUserTokenService userTokenService, IShopAttributeRepository shopAttributeRepository, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
            _userTokenService = userTokenService;
            _userTokenRepository = userTokenRepository;
            _shopAttributeRepository = shopAttributeRepository;
        }

        public async Task<AuthResponseDTO> Handle(ShopRegistrationCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userManager.FindByEmailAsync(request.Email);
            var phoneExists = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

            if (emailExists != null || phoneExists != null) throw new EmailOrPhoneTakenException("Email or Phone already taken");
            if (await _shopAttributeRepository.FindByShopNameAsync(request.ShopName)) throw new EmailOrPhoneTakenException("Email and Phone already taken");

            var user = new User()
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Type = UserType.Shop,
                IsSubscribed = false,
                Logo = "path"
            };

            var shop = new ShopAttribute()
            {
                UserId = user.Id,
                ShopName = request.ShopName,
                Banner = "path"
            };
            
            var refreshToken = await _userTokenService.GenerateRefreshToken();
            var accessToken = await _userTokenService.GenerateJwtToken(user.Id, user.Email, user.FirstName, user.LastName);
            
            user.UserName = user.Id.Replace("-", "");
            
            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new InvalidOperationException("Shop creation failed");

            user.ShopAttributeId = shop.Id;
            await _shopAttributeRepository.CreateAsync(shop);

            user.UserTokenId = refreshToken;
            await _userTokenRepository.CreateAsync(new UserToken(accessToken, refreshToken, DateTime.UtcNow.AddDays(30), user.Id));

            var confirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            var callbackUrl = new UriBuilder(_configuration["WebUrl"]);
            callbackUrl.Path = "/auth/verify-email";
            callbackUrl.Query = $"userid={user.Id}&token={confirmationToken}";
            await _emailService.SendConfirmationEmailAsync(user.Email, callbackUrl.Uri.ToString());

            return new AuthResponseDTO { Type = UserType.Shop, UserId = user.Id, AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}