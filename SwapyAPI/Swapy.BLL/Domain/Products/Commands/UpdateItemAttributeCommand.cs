namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateItemAttributeCommand : UpdateProductCommand
    {
        public string ItemAttributeId { get; set; }
        public bool IsNew { get; set; }
        public string ItemTypeId { get; set; }
    }
}
