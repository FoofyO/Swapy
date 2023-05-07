namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateClothesAttributeCommand : UpdateProductCommand
    {
        public Guid ClothesAttributeId { get; set; }
        public bool IsNew { get; set; }
        public Guid ClothesSeasonId { get; set; }
        public Guid ClothesSizeId { get; set; }
        public Guid ClothesBrandViewId { get; set; }
    }
}
