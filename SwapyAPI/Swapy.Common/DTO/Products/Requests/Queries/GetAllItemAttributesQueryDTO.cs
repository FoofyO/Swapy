using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllItemAttributesQueryDTO : GetAllProductQueryDTO<ItemAttribute>
    {
        public bool? isNew { get; set; }
        public List<string> itemTypesId { get; set; }
    }
}
