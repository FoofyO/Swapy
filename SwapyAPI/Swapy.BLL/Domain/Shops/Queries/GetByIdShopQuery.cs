using MediatR;
using Swapy.Common.DTO.Shops.Responses;

namespace Swapy.BLL.Domain.Shops.Queries
{
    public class GetByIdShopQuery : IRequest<ShopDetailResponseDTO>
    {
        public string ShopId { get; set; }
    }
}
