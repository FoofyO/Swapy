using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveElectronicAttributeCommand : IRequest<Unit>
    {
        public Guid ElectronicAttribute { get; set; }
    }
}
