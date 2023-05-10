namespace Swapy.Common.Entities
{
    public class Category
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

        public Category() { }

        public Category(string name) => Name = name;
    }
}
