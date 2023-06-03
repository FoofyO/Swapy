using MediatR;

namespace Swapy.BLL.Domain.Clothes.Commands
{
    public class RemoveClothesAttributeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string ClothesAttributeId { get; set; }
    }
}
