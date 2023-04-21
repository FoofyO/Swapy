namespace Swapy.DAL.Entities
{
    public class Subcategory
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid PrevSubcategoryId { get; set; }
        public Subcategory PrevSubcategory { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
        public ICollection<Product> Products { get; set; }

        public Subcategory() {
            Products = new List<Product>();
            Subcategories = new List<Subcategory>();
        }

        public Subcategory(string name, Guid categoryId, Guid prevSubcategoryId)
        {
            Name = name;
            CategoryId = categoryId;
            PrevSubcategoryId = prevSubcategoryId
        }
    }
}