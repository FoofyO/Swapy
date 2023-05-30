using Swapy.Common.DTO.Products.Responses;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllItemAttributesQuery : GetAllProductQuery<ProductResponseDTO>
    {
        public bool? IsNew { get; set; }
        public List<string> ItemTypesId { get; set; }
    }
}
