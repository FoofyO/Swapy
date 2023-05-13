using MediatR;
using Swapy.BLL.Domain.Shops.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Shops.QueryHandlers
{
    public class GetByIdShopQueryHandler : IRequestHandler<GetByIdShopQuery, ShopAttribute>
    {
        private readonly IShopAttributeRepository _shopAttributeRepository;

        public GetByIdShopQueryHandler(IShopAttributeRepository shopAttributeRepository) => _shopAttributeRepository = shopAttributeRepository;

        public async Task<ShopAttribute> Handle(GetByIdShopQuery request, CancellationToken cancellationToken)
        {
            return await _shopAttributeRepository.GetByIdAsync(request.ShopId);
        }
    }
}
