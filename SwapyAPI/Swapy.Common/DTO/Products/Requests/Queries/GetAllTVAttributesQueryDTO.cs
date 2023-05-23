using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllTVAttributesQueryDTO : GetAllProductQueryDTO<TVAttribute>
    {
        public bool? IsNew { get; set; }
        public bool? IsSmart { get; set; }
        public List<string> TVTypesId { get; set; }
        public List<string> TVBrandsId { get; set; }
        public List<string> ScreenResolutionsId { get; set; }
        public List<string> ScreenDiagonalsId { get; set; }
    }
}
