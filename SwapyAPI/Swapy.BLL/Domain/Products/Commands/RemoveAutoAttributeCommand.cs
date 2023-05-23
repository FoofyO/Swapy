using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveAutoAttributeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string AutoAttributeId { get; set; }
    }
}
