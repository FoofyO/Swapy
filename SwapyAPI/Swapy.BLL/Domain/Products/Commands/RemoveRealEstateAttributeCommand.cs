using MediatR;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class RemoveRealEstateAttributeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string RealEstateAttributeId { get; set; }
    }
}
