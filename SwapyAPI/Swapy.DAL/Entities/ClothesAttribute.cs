namespace Swapy.DAL.Entities
{
    public class ClothesAttribute 
    {
        public Guid Id { get; set; }
        public bool IsNew { get; set; } 
        public Guid ClothesSeasonId { get; set; }
        public ClothesSeason ClothesSeason { get; set; }
        public Guid ClothesSizeId { get; set; }
        public ClothesSize ClothesSize { get; set; }
        public Guid ClothesBrandViewId{ get; set; }
        public ClothesBrandView ClothesBrandView { get; set; }
        public Guid ProductId { get; set; } 
        public Product Product { get; set; }

        public ClothesAttribute() { } 
              
        public ClothesAttribute(bool isNew, Guid clothesSeasonId, Guid clothesSizeId, Guid clothesBrandViewId, Guid productId)
        { 
            IsNew = isNew;
            ClothesSeasonId = clothesSeasonId;
            ClothesSizeId = clothesSizeId;
            ClothesBrandViewId = clothesBrandViewId;
            ProductId = productId;
        }
    }
}
