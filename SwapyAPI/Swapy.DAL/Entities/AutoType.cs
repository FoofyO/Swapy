namespace Swapy.DAL.Entities
{
    public class AutoType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoBrandType> AutoBrandsTypes { get; set; }
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public AutoType() => AutoBrandsTypes = new List<AutoBrandType>();

        public AutoType(string name, Guid subcategoryId) : this()
        {
            Name = name;
            SubcategoryId = subcategoryId;
        }
    }
}
