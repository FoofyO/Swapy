namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddItemAttributeCommand : AddProductCommand
    {
        public bool IsNew { get; set; }
        public string ItemTypeId { get; set; }
    }
}
