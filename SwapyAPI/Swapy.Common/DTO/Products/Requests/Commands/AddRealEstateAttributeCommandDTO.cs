using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddRealEstateAttributeCommandDTO : AddProductCommandDTO<RealEstateAttribute>
    {
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
        public string RealEstateTypeId { get; set; }
    }
}
