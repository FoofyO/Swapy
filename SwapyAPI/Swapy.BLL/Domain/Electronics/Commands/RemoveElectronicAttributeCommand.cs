using MediatR;

namespace Swapy.BLL.Domain.Electronics.Commands
{
    public class RemoveElectronicAttributeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string ElectronicAttributeId { get; set; }
    }
}
