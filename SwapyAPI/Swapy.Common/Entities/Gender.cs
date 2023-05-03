namespace Swapy.Common.Entities
{
    public class Gender
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClothesView> ClothesViews { get; set; }

        public Gender() => ClothesViews = new List<ClothesView>();

        public Gender(string name) : this() => Name = name;
    }
}
 