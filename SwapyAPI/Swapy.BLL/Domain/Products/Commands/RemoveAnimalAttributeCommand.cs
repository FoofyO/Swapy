using MediatR;

namespace Swapy.BLL.CQRS.Commands
{
    public class RemoveAnimalAttributeCommand : IRequest<Unit>
    {
        public Guid AnimalAttributeId { get; set; }
    }
}
