using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllTVAttributesQueryDTO : GetAllProductQueryDTO<TVAttribute>
    {
        public bool? isNew { get; set; }
        public bool? isSmart { get; set; }
        public List<string> tvTypesId { get; set; }
        public List<string> tvBrandsId { get; set; }
        public List<string> screenResolutionsId { get; set; }
        public List<string> screenDiagonalsId { get; set; }
    }
}
