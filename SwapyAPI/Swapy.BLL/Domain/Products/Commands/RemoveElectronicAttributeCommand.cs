using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveElectronicAttributeCommand : IRequest<Unit>
    {
        public string ElectronicAttribute { get; set; }
    }
}
