using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveAnimalAttributeCommand : IRequest<Unit>
    {
        public Guid AnimalAttributeId { get; set; }
    }
}
