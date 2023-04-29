namespace Swapy.DAL.Entities
{
    public class ClothesBrandView
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ClothesBrandId { get; set; }
        public ClothesBrand ClothesBrand { get; set; }
        public Guid ClothesViewId { get; set; }
        public ClothesView ClothesView { get; set; }

        public ICollection<ClothesAttribute> ClothesAttributes { get; set; }

        public ClothesBrandView() => ClothesAttributes = new List<ClothesAttribute>();
          
        public ClothesBrandView(string name, Guid clothesBrandId, Guid clothesViewId) : this() 
        {  
            Name = name; 
            ClothesBrandId = clothesBrandId;
            ClothesViewId = clothesViewId;
        } 
    }

} 