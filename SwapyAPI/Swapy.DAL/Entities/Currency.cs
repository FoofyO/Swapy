namespace Swapy.DAL.Entities
{
    public class Currency
    {
        public Guid Id { get; set; }
        public string Name { get; set; }    
        public string Symbol { get; set; }
        public ICollection<Product> Products { get; set; }

        public Currency() => Products = new List<Product>();
        public Currency(string name, string symbol) : this()
        {
            Name = name;
            Symbol = symbol;
        }
    }
}
