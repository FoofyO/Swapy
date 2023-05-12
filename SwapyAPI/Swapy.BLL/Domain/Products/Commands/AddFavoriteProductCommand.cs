using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddFavoriteProductCommand : AddProductCommand<FavoriteProduct>
    {
        public string ProductId { get; set; }
    }
}
