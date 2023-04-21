namespace Swapy.DAL.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }

        public Category() => Subcategories = new List<Subcategory>();

        public Category(string name) => Name = name;
    }
}
