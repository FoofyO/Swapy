using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllRealEstateAttributesQueryDTO : GetAllProductQueryDTO<RealEstateAttribute>
    {
        public int? areaMin { get; set; }
        public int? areaMax { get; set; }
        public int? roomsMin { get; set; }
        public int? roomsMax { get; set; }
        public bool? isRent { get; set; }
        public List<string> realEstateTypesId { get; set; }
    }
}
