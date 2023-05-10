using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveClothesAttributeCommand : IRequest<Unit>
    {
        public string ClothesAttributeId { get; set; }
    }
}
