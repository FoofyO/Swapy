namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateRealEstateAttributeCommand : UpdateProductCommand
    {
        public Guid RealEstateAttributeId { get; set; }
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
        public Guid RealEstateTypeId { get; set; }
    }
}
