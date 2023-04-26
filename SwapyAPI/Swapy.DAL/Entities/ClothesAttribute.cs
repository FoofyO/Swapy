namespace Swapy.DAL.Entities
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; }
        public int Reviews { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid CurrencyId { get; set; }
        public Currency Currency { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public Guid CityId { get; set; }
        public City City { get; set; }
<<<<<<< HEAD
        public Guid AutoAttributeId { get; set; }
        public AutoAttribute AutoAttribute { get; set; }
        public Guid ElectronicAttributeId { get; set; }
        public ElectronicAttribute ElectronicAttribute { get; set; }
        public Guid ItemAttributeId { get; set; }
        public ItemAttribute ItemAttribute { get; set; }
=======
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        public ICollection<ProductImage> Images { get; set; }

        public Product() => Images = new List<ProductImage>();

<<<<<<< HEAD
        public Product(string title, string description, decimal price, Guid userId, Guid currencyId, Guid categoryId, Guid subcategoryId, Guid cityId, Guid autoAttributeId, Guid electronicAttributeId, Guid itemAttributeId) : this()
=======
        public Product(string title, string description, decimal price, Guid userId, Guid currencyId, Guid categoryId, Guid subcategoryId, Guid cityId)
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        {
            Title = title;
            Description = description;
            Price = price;
            UserId = userId;
            CurrencyId = currencyId;
            SubcategoryId = subcategoryId;
            CityId = cityId;
<<<<<<< HEAD
            AutoAttributeId = autoAttributeId;
            ElectronicAttributeId = electronicAttributeId;
            ItemAttributeId = itemAttributeId;
=======
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        }
    }
}
