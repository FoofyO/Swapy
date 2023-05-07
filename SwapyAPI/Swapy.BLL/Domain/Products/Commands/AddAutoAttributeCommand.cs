namespace Swapy.BLL.CQRS.Commands
{
    public class AddAutoAttributeCommand : AddProductCommand
    {
        public int Miliage { get; set; }
        public int EngineCapacity { get; set; }
        public DateTime ReleaseYear { get; set; }
        public bool IsNew { get; set; }
        public Guid FuelTypeId { get; set; }
        public Guid AutoColorId { get; set; }
        public Guid TransmissionTypeId { get; set; }
        public Guid AutoBrandTypeId { get; set; }
    }
}
