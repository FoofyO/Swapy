namespace Swapy.Common.Entities
{
    public class ElectronicBrand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<ElectronicBrandType> ElectronicBrandsTypes { get; set; } = new List<ElectronicBrandType>();

        public ElectronicBrand() { }

        public ElectronicBrand(string name) => Name = name;
    }
}
