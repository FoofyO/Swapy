namespace Swapy.DAL.Entities
{
    public class ClothesTypes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClothesView> ClothesViews { get; set; }

        public ClothesTypes() => ClothesViews = new List<ClothesView>();

        public ClothesTypes(string name) : this() => Name = name;

        public ClothesTypes(string name) => Name = name; 
    }
} 
 