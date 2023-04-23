namespace Swapy.DAL.Entities
{
    public class AutoBrand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoBrandType> AutoBrandsTypes { get; set; }

        public AutoBrand() => AutoBrandsTypes = new List<AutoBrandType>();

        public AutoBrand(string name) : this() => Name = name;
    }
}
