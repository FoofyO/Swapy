using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddClothesAttributeCommandDTO : AddProductCommandDTO<ClothesAttribute>
    {
        public bool IsNew { get; set; }
        public string ClothesSeasonId { get; set; }
        public string ClothesSizeId { get; set; }
        public string ClothesBrandViewId { get; set; }
    }
}
