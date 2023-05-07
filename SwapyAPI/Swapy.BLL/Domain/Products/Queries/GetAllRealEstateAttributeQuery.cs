using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllRealEstateAttributeQuery : GetAllProductQuery<RealEstateAttribute>
    {
        public int? AreaMin { get; set; }
        public int? AreaMax { get; set; }
        public int? RoomsMin { get; set; }
        public int? RoomsMax { get; set; }
        public bool? IsRent { get; set; }
        public List<Guid> RealEstateTypesId { get; set; }
    }
}
