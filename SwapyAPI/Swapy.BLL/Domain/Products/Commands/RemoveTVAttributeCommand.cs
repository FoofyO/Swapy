using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveTVAttributeCommand : IRequest<Unit>
    {
        public Guid TVAttributeId { get; set; }
    }
}
