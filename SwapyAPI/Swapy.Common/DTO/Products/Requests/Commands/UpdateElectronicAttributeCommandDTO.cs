namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateElectronicAttributeCommandDTO : UpdateProductCommandDTO
    {
        public bool isNew { get; set; }
        public string memoryModelId { get; set; }
        public string modelColorId { get; set; }
    }
}
