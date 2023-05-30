using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllClothesAttributesQueryDTO : GetAllProductQueryDTO<ClothesAttribute>
    {
        public bool? isNew { get; set; }
        public List<string> clothesSeasonsId { get; set; }
        public List<string> clothesSizesId { get; set; }
        public List<string> clothesBrandsId { get; set; }
        public List<string> clothesViewsId { get; set; }
        public List<string> clothesTypesId { get; set; }
        public List<string> clothesGendersId { get; set; }
    }
}
