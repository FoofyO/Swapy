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
<<<<<<< HEAD
        public Guid AutoTypeId { get; set; }
        public AutoType AutoType { get; set; }
        public Guid ItemTypeId { get; set; }
        public ItemType ItemType { get; set; }
=======
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        public ICollection<Subcategory> Subcategories { get; set; }
        public ICollection<Product> Products { get; set; }

        public Subcategory() {
            Products = new List<Product>();
            Subcategories = new List<Subcategory>();
        }

<<<<<<< HEAD
        public Subcategory(string name, Guid categoryId, Guid prevSubcategoryId, Guid autoTypeId, Guid itemTypeId) : this()
        {
            Name = name;
            CategoryId = categoryId;
            PrevSubcategoryId = prevSubcategoryId;
            AutoTypeId = autoTypeId;
            ItemTypeId = itemTypeId;
=======
        public Subcategory(string name, Guid categoryId, Guid prevSubcategoryId)
        {
            Name = name;
            CategoryId = categoryId;
            PrevSubcategoryId = prevSubcategoryId
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        }
    }
}