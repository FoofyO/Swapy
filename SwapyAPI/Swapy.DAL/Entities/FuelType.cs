namespace Swapy.DAL.Entities
{
    public class FuelType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; }

        public FuelType() => AutoAttributes = new List<AutoAttribute>();

        public FuelType(string name) : this() => Name = name;
    }
}
