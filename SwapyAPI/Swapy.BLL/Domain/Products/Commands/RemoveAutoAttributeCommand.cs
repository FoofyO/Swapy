using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveAutoAttributeCommand : IRequest<Unit>
    {
        public string AutoAttributeId { get; set; }
    }
}
