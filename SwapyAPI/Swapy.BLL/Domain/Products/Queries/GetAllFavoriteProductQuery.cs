using Swapy.Common.Entities;
using System.IO;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllFavoriteProductQuery : GetAllProductQuery<FavoriteProduct>
    {
        public string ProductId { get; set; }
    }
}
