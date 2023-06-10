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
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class ShopRegistrationCommandHandler : IRequestHandler<ShopRegistrationCommand, AuthResponseDTO>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserTokenService _userTokenService;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public ShopRegistrationCommandHandler(UserManager<User> userManager, IUserTokenRepository userTokenRepository, IUserTokenService userTokenService, IShopAttributeRepository shopAttributeRepository)
        {
            _userManager = userManager;
            _userTokenService = userTokenService;
            _userTokenRepository = userTokenRepository;
            _shopAttributeRepository = shopAttributeRepository;
        }

        public async Task<AuthResponseDTO> Handle(ShopRegistrationCommand request, CancellationToken cancellationToken)
        {
            var emailExists = await _userManager.FindByEmailAsync(request.Email);
            var phoneExists = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);

            if (emailExists != null || phoneExists != null) throw new EmailOrPhoneTakenException("Email and Phone already taken");
            if (await _shopAttributeRepository.FindByShopNameAsync(request.ShopName)) throw new EmailOrPhoneTakenException("Email and Phone already taken");

            var user = new User()
            {
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                Type = UserTypes.Shop,
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

            return new AuthResponseDTO { Type = UserTypes.Shop, UserId = user.Id, AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}