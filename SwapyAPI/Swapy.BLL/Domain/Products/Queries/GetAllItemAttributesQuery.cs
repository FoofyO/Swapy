using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllItemAttributesQuery : GetAllProductQuery<ItemAttribute>
    {
        public bool? IsNew { get; set; }
        public List<string> ItemTypesId { get; set; }
    }
}
