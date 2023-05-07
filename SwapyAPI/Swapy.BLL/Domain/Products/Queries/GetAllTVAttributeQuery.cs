using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllTVAttributeQuery : GetAllProductQuery<TVAttribute>
    {
        public bool? IsNew { get; set; }
        public bool? IsSmart { get; set; }
        public List<Guid> TVTypesId { get; set; }
        public List<Guid> TVBrandsId { get; set; }
        public List<Guid> ScreenResolutionsId { get; set; }
        public List<Guid> ScreenDiagonalsId { get; set; }
    }
}
