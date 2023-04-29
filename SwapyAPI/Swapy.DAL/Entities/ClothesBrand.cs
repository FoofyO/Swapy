namespace Swapy.DAL.Entities
{
    public class ClothesBrand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClothesBrandView> ClothesBrandsViews { get; set; }

        public ClothesBrand() => ClothesBrandsViews = new List<ClothesBrandView>();
         
        public ClothesBrand(string name) : this() => Name = name;
    } 
}
 