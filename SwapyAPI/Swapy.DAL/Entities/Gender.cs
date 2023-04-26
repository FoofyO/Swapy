namespace Swapy.DAL.Entities
{
    public class City
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

        public City() => Products = new List<Product>();
        
<<<<<<< HEAD
        public City(string name) : this() => Name = name;
=======
        public City(string name) => Name = name;
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
    } 
}
