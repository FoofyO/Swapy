using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddItemAttributeCommand : AddProductCommand<ItemAttribute>
    {
        public bool IsNew { get; set; }
        public string ItemTypeId { get; set; }
    }
}
