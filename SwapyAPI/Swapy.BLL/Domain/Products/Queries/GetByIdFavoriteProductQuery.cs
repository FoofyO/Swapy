using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdFavoriteProductQuery : IRequest<FavoriteProduct>
    {
        public string FavoriteProductId { get; set; }
    }
}
