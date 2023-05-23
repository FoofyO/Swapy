using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddItemAttributeCommandDTO : AddProductCommandDTO<ItemAttribute>
    {
        public bool IsNew { get; set; }
        public string ItemTypeId { get; set; }
    }
}
