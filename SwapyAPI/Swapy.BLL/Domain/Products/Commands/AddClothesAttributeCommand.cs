namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddClothesAttributeCommand : AddProductCommand
    {
        public bool IsNew { get; set; }
        public Guid ClothesSeasonId { get; set; }
        public Guid ClothesSizeId { get; set; }
        public Guid ClothesBrandViewId { get; set; }
    }
}
