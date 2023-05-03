namespace Swapy.Common.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

        public City() => Products = new List<Product>();
        
        public City(string name) : this() => Name = name;
    } 
}
