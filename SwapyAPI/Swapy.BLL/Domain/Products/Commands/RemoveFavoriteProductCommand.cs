using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveFavoriteProductCommand : IRequest<Unit>
    {
        public string FavoriteProductId { get; set; }
    }
}
