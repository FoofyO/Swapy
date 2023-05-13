using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllClothesAttributesQuery : GetAllProductQuery<ClothesAttribute>
    {
        public bool? IsNew { get; set; }
        public List<string> ClothesSeasonsId { get; set; }
        public List<string> ClothesSizesId { get; set; }
        public List<string> ClothesBrandsId { get; set; }
        public List<string> ClothesViewsId { get; set; }
        public List<string> ClothesTypesId { get; set; }
        public List<string> ClothesGendersId { get; set; }
    }
}
