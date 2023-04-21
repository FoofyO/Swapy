namespace Swapy.DAL.Entities
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }

        public Category()
        {
            Products = new List<Product>();
            Subcategories = new List<Subcategory>();
        }

        public Category(string name) => Name = name;
    }
}
