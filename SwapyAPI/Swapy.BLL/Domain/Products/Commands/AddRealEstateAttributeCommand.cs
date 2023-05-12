using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddRealEstateAttributeCommand : AddProductCommand<RealEstateAttribute>
    {
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
        public string RealEstateTypeId { get; set; }
    }
}
