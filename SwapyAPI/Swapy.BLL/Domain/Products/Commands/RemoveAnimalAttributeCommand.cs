using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveAnimalAttributeCommand : IRequest<Unit>
    {
        public string AnimalAttributeId { get; set; }
    }
}
