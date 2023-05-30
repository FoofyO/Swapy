using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddClothesAttributeCommandDTO : AddProductCommandDTO<ClothesAttribute>
    {
        public bool isNew { get; set; }
        public string clothesSeasonId { get; set; }
        public string clothesSizeId { get; set; }
        public string clothesBrandViewId { get; set; }
    }
}
