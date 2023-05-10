namespace Swapy.Common.Entities
{
    public class City
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public City() { }

        public City(string name) => Name = name;
    } 
}
