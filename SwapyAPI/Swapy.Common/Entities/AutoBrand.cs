namespace Swapy.Common.Entities
{
    public class AutoBrand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoBrandType> AutoBrandsTypes { get; set; } = new List<AutoBrandType>();

        public AutoBrand() => Id = Guid.NewGuid().ToString();

        public AutoBrand(string name) : this() => Name = name;
    }
}
