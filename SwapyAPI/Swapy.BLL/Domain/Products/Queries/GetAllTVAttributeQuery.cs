using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllTVAttributeQuery : GetAllProductQuery<TVAttribute>
    {
        public bool? IsNew { get; set; }
        public bool? IsSmart { get; set; }
        public List<string> TVTypesId { get; set; }
        public List<string> TVBrandsId { get; set; }
        public List<string> ScreenResolutionsId { get; set; }
        public List<string> ScreenDiagonalsId { get; set; }
    }
}
