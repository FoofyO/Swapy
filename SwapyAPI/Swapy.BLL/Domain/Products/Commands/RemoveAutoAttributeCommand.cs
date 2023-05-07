using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveAutoAttributeCommand : IRequest<Unit>
    {
        public Guid AutoAttributeId { get; set; }
    }
}
