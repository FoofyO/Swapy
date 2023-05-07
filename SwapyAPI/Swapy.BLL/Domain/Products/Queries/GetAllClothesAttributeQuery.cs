using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesAttributeQuery : GetAllProductQuery<ClothesAttribute>
    {
        public bool? IsNew { get; set; }
        public List<Guid> ClothesSeasonsId { get; set; }
        public List<Guid> ClothesSizesId { get; set; }
        public List<Guid> ClothesBrandsId { get; set; }
        public List<Guid> ClothesViewsId { get; set; }
        public List<Guid> ClothesTypesId { get; set; }
        public List<Guid> ClothesGendersId { get; set; }
    }
}
