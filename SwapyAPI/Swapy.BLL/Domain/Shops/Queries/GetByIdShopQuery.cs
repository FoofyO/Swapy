using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Shops.Queries
{
    public class GetByIdShopQuery : IRequest<ShopAttribute>
    {
        public string ShopId { get; set; }
    }
}
