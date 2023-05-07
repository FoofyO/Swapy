namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateItemAttributeCommand : UpdateProductCommand
    {
        public Guid ItemAttributeId { get; set; }
        public bool IsNew { get; set; }
        public Guid ItemTypeId { get; set; }
    }
}
