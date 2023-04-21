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
        public ICollection<ProductImage> Images { get; set; }

        public Product() => Images = new List<ProductImage>();

        public Product(string title, string description, decimal price, Guid userId, Guid currencyId, Guid categoryId, Guid subcategoryId, Guid cityId)
        {
            Title = title;
            Description = description;
            Price = price;
            UserId = userId;
            CurrencyId = currencyId;
            SubcategoryId = subcategoryId;
            CityId = cityId;
        }
    }
}
