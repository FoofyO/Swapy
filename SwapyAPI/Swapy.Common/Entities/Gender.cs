namespace Swapy.Common.Entities
{
    public class Gender
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ClothesView> ClothesViews { get; set; } = new List<ClothesView>();

        public Gender() { }

        public Gender(string name) => Name = name;
    }
}
 