namespace Swapy.Common.Entities
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
        public Guid AutoAttributeId { get; set; }
        public AutoAttribute AutoAttribute { get; set; }
        public Guid AnimalAttributeId { get; set; }
        public AnimalAttribute AnimalAttribute { get; set; }
        public Guid ClothesAttributeId { get; set; }
        public ClothesAttribute ClothesAttribute { get; set; }
        public Guid TVAttributeId { get; set; }
        public TVAttribute TVAttribute { get; set; }
        public Guid RealEstateAttributeId { get; set; }
        public RealEstateAttribute RealEstateAttribute { get; set; }
        public Guid ElectronicAttributeId { get; set; }
        public ElectronicAttribute ElectronicAttribute { get; set; }
        public Guid ItemAttributeId { get; set; }
        public ItemAttribute ItemAttribute { get; set; }
        public ICollection<Chat> Chats { get; set; }
        public ICollection<ProductImage> Images { get; set; }
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }

        public Product()
        {
            Chats = new List<Chat>();
            Images = new List<ProductImage>();
            FavoriteProducts = new List<FavoriteProduct>();
        }

        public Product(string title, string description, decimal price, Guid userId, Guid currencyId, Guid categoryId, Guid subcategoryId, Guid cityId) : this()
        {
            Title = title;
            Description = description;
            Price = price;
            UserId = userId;
            DateTime = DateTime.Now;
            Reviews = 0;
            CurrencyId = currencyId;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            CityId = cityId;
        }

        public Product(string title, string description, decimal price, Guid userId, Guid currencyId, Guid categoryId, Guid subcategoryId, Guid cityId, Guid autoAttributeId, Guid animalAttributeId, Guid clothesAttributeId, Guid tVAttributeId, Guid realEstateAttributeId, Guid electronicAttributeId, Guid itemAttributeId) : this()
        {
            Title = title;
            Description = description;
            Price = price;
            UserId = userId;
            DateTime = DateTime.Now;
            Reviews = 0;
            CurrencyId = currencyId;
            CategoryId = categoryId;
            SubcategoryId = subcategoryId;
            CityId = cityId;
            AutoAttributeId = autoAttributeId;
            AnimalAttributeId = animalAttributeId;
            ClothesAttributeId = clothesAttributeId;
            TVAttributeId = tVAttributeId;
            RealEstateAttributeId = realEstateAttributeId;
            ElectronicAttributeId = electronicAttributeId;
            ItemAttributeId = itemAttributeId;
        }
    }
}
