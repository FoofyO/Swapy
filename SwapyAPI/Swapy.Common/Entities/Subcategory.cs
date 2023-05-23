using Swapy.Common.Enums;

namespace Swapy.Common.Entities
{
    public class Subcategory
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public SubcategoryType Type { get; set; }
        public string CategoryId { get; set; }
        public Category Category { get; set; }
        public string ParentSubcategoryId { get; set; }
        public SubcategoryBranch ParentSubcategory { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<AnimalBreed> AnimalBreeds { get; set; } = new List<AnimalBreed>();
        public ICollection<ClothesView> ClothesViews { get; set; } = new List<ClothesView>();
        public ICollection<ItemAttribute> ItemAttributes { get; set; } = new List<ItemAttribute>();
        public ICollection<AutoModel> AutoModels { get; set; } = new List<AutoModel>();
        public ICollection<SubcategoryBranch> ChildSubcategories { get; set; } = new List<SubcategoryBranch>();
        public ICollection<RealEstateAttribute> RealEstateAttributes { get; set; } = new List<RealEstateAttribute>();
        public ICollection<ElectronicBrandType> ElectronicBrandsTypes { get; set; } = new List<ElectronicBrandType>();

        public Subcategory() => Id = Guid.NewGuid().ToString();
             
        public Subcategory(string name, SubcategoryType type, string categoryId, string parentSubcategoryId) : this()
        {
            Name = name;
            Type = type;
            CategoryId = categoryId;
            ParentSubcategoryId = parentSubcategoryId;
        }
    }
}