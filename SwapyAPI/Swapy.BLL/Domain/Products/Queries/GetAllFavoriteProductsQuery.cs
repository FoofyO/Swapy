using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllFavoriteProductsQuery : GetAllProductQuery<FavoriteProduct>
    {
        public string ProductId { get; set; }
    }
}
