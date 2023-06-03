using MediatR;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.DTO.Shops.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.QueryHandlers
{
    public class GetByIdShopQueryHandler : IRequestHandler<GetByIdShopQuery, ShopDetailResponseDTO>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public GetByIdShopQueryHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<ShopDetailResponseDTO> Handle(GetByIdShopQuery request, CancellationToken cancellationToken)
        {
            var shop = await _shopAttributeRepository.GetByUserIdAsync(request.UserId);
            return new ShopDetailResponseDTO()
            {
                Id = shop.Id,
                Views = shop.Views,
                Banner = shop.Banner,
                Slogan = shop.Slogan,
                UserId = shop.UserId,
                Logo = shop.User.Logo,
                Email = shop.User.Email,
                ShopName = shop.ShopName,
                Location = shop.Location,
                WorkDays = shop.WorkDays,
                Description = shop.Description,
                EndWorkTime = shop.EndWorkTime,
                LikesCount = shop.User.LikesCount,
                StartWorkTime = shop.StartWorkTime,
                PhoneNumber = shop.User.PhoneNumber,
                ProductsCount = shop.User.ProductsCount,
                RegistrationDate = shop.User.RegistrationDate,
                SubscriptionsCount = shop.User.SubscriptionsCount
            };
        }
    }
}
