using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllItemAttributesQueryDTO : GetAllProductQueryDTO<ItemAttribute>
    {
        public bool? IsNew { get; set; }
        public List<string> ItemTypesId { get; set; }
    }
}
