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
        public ICollection<Product> Products { get; set; }
        public ICollection<AnimalBreed> AnimalBreeds { get; set; }
        public ICollection<ClothesView> ClothesViews { get; set; }
        public ICollection<Subcategory> Subcategories { get; set; }
        public ICollection<ItemAttribute> ItemAttributes { get; set; }
        public ICollection<AutoBrandType> AutoBrandTypes { get; set; }
        public ICollection<ElectronicBrandType> ElectronicBrandsTypes { get; set; }
        public ICollection<RealEstateAttribute> RealEstateAttributes { get; set; }

        public Subcategory() 
        {
            Products = new List<Product>();
            ClothesViews = new List<ClothesView>();
            AnimalBreeds = new List<AnimalBreed>();
            Subcategories = new List<Subcategory>();
            AutoBrandTypes= new List<AutoBrandType>();
            ItemAttributes = new List<ItemAttribute>();
            RealEstateAttributes = new List<RealEstateAttribute>();
            ElectronicBrandsTypes = new List<ElectronicBrandType>();
        }

        public Subcategory(string name, Guid categoryId, Guid prevSubcategoryId, ICollection<RealEstateAttribute> realEstateAttributes) : this()
        {
            Name = name;
            CategoryId = categoryId;
            PrevSubcategoryId = prevSubcategoryId;
            RealEstateAttributes = realEstateAttributes;
        }
    }
}