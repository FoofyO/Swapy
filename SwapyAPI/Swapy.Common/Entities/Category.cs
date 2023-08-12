using Swapy.Common.Enums;

namespace Swapy.Common.Entities
{
    public class Category
    {
        public string Id { get; set; }
        public CategoryType Type { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>();

        public Category() => Id = Guid.NewGuid().ToString();

        public Category(CategoryType type) : this() => Type = type;
    }
}
