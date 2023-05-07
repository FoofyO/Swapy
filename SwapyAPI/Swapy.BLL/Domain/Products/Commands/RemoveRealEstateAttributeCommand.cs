using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveRealEstateAttributeCommand : IRequest<Unit>
    {
        public Guid RealEstateAttributeId { get; set; }
    }
}
