namespace Swapy.DAL.Entities
{
    public class ElectronicBrand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ElectronicBrandType> ElectronicBrandsTypes { get; set; }

        public ElectronicBrand() => ElectronicBrandsTypes = new List<ElectronicBrandType>();

        public ElectronicBrand(string name) : this() => Name = name;
    }
}
