namespace Swapy.DAL.Entities
{
    public class Genders
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClothesView> ClothesViews { get; set; }

        public Genders() => ClothesViews = new List<ClothesView>();

        public Genders(string name) : this() => Name = name;

        public Genders(string name) => Name = name;
    }
}
 