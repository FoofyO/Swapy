using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveItemAttributeCommand : IRequest<Unit>
    {
        public string ItemAttributeId { get; set; }
    }
}
