namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateElectronicAttributeCommandDTO : UpdateProductCommandDTO
    {
        public string ElectronicAttributeId { get; set; }
        public bool IsNew { get; set; }
        public string MemoryModelId { get; set; }
        public string ModelColorId { get; set; }
    }
}
