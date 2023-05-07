namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateAutoAttributeCommand : UpdateProductCommand
    {
        public Guid AutoAttributeId { get; set; }
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
