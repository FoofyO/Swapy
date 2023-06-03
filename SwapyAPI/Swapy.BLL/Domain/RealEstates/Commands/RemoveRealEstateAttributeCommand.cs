using MediatR;

namespace Swapy.BLL.Domain.RealEstates.Commands
{
    public class RemoveRealEstateAttributeCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public string RealEstateAttributeId { get; set; }
    }
}
