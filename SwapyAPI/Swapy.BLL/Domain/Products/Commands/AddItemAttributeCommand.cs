namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddItemAttributeCommand : AddProductCommand
    {
        public bool IsNew { get; set; }
        public Guid ItemTypeId { get; set; }
    }
}
