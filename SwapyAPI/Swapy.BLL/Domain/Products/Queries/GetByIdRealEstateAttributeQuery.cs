using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetByIdRealEstateAttributeQuery : IRequest<RealEstateAttribute>
    {
        public string RealEstateAttributeId { get; set; }
    }
}
