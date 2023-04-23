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

<<<<<<< HEAD
        public Category(string name) : this() => Name = name;
=======
        public Category(string name) => Name = name;
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
    }
}
