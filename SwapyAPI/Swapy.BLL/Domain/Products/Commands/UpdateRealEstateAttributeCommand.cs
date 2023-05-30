namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateRealEstateAttributeCommand : UpdateProductCommand
    {
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
        public string RealEstateTypeId { get; set; }
    }
}
