using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllItemAttributeQuery : GetAllProductQuery<ItemAttribute>
    {
        public bool? IsNew { get; set; }
        public List<string> ItemTypesId { get; set; }
    }
}
