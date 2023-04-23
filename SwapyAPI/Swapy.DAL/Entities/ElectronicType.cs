namespace Swapy.DAL.Entities
{
    public class ElectronicType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ElectronicBrandType> ElectronicBrandsTypes { get; set; }

        public ElectronicType() => ElectronicBrandsTypes = new List<ElectronicBrandType>();

        public ElectronicType(string name) : this() => Name = name;
    }
}
