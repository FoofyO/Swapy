using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveTVAttributeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string TVAttributeId { get; set; }
    }
}
