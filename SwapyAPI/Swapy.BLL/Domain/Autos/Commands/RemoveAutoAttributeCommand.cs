using MediatR;

namespace Swapy.BLL.Domain.Autos.Commands
{
    public class RemoveAutoAttributeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string AutoAttributeId { get; set; }
    }
}
