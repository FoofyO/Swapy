using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveItemAttributeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string ItemAttributeId { get; set; }
    }
}
