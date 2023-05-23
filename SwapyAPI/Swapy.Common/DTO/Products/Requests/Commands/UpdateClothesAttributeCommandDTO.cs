namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class UpdateClothesAttributeCommandDTO : UpdateProductCommandDTO
    {
        public string ClothesAttributeId { get; set; }
        public bool IsNew { get; set; }
        public string ClothesSeasonId { get; set; }
        public string ClothesSizeId { get; set; }
        public string ClothesBrandViewId { get; set; }
    }
}
