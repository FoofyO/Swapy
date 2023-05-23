using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllFavoriteProductsQueryDTO : GetAllProductQueryDTO<FavoriteProduct>
    {
        public string ProductId { get; set; }
    }
}
