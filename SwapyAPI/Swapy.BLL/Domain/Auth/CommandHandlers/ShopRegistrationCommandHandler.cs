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
    public class ShopRegistrationCommandHandler : IRequestHandler<ShopRegistrationCommand, AuthResponseDTO>
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public ShopRegistrationCommandHandler(UserManager<User> userManager, IRefreshTokenRepository refreshTokenRepository, ITokenService tokenService, IShopAttributeRepository shopAttributeRepository)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _refreshTokenRepository = refreshTokenRepository;
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
                Type = UserType.Shop,
                Logo = "path"
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded) throw new InvalidOperationException("Shop creation failed");

            var shop = new ShopAttribute()
            {
                ShopName = request.ShopName,
                UserId = user.Id,
                Banner = "path"
            };

            await _shopAttributeRepository.CreateAsync(shop);

            var refreshToken = await _tokenService.GenerateRefreshToken();
            var accessToken = await _tokenService.GenerateJwtToken(user);

            await _refreshTokenRepository.CreateAsync(new RefreshToken(refreshToken, DateTime.UtcNow.AddDays(30), user.Id));

            var authDTO = new AuthResponseDTO { UserId = user.Id, AccessToken = accessToken, RefreshToken = refreshToken };
            return new AuthResponseDTO();
        }
    }
}