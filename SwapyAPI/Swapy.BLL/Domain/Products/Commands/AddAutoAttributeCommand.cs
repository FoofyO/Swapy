namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddAutoAttributeCommand : AddProductCommand
    {
        public int Miliage { get; set; }
        public int EngineCapacity { get; set; }
        public DateTime ReleaseYear { get; set; }
        public bool IsNew { get; set; }
        public string FuelTypeId { get; set; }
        public string AutoColorId { get; set; }
        public string TransmissionTypeId { get; set; }
        public string AutoBrandTypeId { get; set; }
    }
}
