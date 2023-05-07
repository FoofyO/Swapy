namespace Swapy.BLL.CQRS.Commands
{
    public class AddRealEstateAttributeCommand : AddProductCommand
    {
        public int Area { get; set; }
        public int Rooms { get; set; }
        public bool IsRent { get; set; }
        public Guid RealEstateTypeId { get; set; }
    }
}
