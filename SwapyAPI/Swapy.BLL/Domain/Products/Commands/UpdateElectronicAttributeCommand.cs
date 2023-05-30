namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateElectronicAttributeCommand : UpdateProductCommand
    {
        public bool IsNew { get; set; }
        public string MemoryModelId { get; set; }
        public string ModelColorId { get; set; }
    }
}
