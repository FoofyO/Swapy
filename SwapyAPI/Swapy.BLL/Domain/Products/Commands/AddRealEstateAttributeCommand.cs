namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddRealEstateAttributeCommand : AddProductCommand
    {
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
        public string RealEstateTypeId { get; set; }
    }
}
