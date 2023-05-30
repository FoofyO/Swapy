using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddItemAttributeCommandDTO : AddProductCommandDTO<ItemAttribute>
    {
        public bool isNew { get; set; }
        public string itemTypeId { get; set; }
    }
}
