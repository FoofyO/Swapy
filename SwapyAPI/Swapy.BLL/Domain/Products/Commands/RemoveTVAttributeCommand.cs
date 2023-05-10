using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveTVAttributeCommand : IRequest<Unit>
    {
        public string TVAttributeId { get; set; }
    }
}
