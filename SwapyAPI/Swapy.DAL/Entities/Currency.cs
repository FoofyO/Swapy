namespace Swapy.DAL.Entities
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public string Symbol { get; set; }
        public ICollection<Product> Products { get; set; }

        public Currency() => Products = new List<Product>();

<<<<<<< HEAD
        public Currency(string name, string symbol) : this()
=======
        public Currency(string name, string symbol)
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        {
            Name = name;
            Symbol = symbol;
        }
    }
}
