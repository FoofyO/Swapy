﻿namespace Swapy.Common.Entities
{
    public class Currency
    {
        public string Id { get; set; }
        public string Name { get; set; }    
        public string Symbol { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();

        public Currency() { }

        public Currency(string name, string symbol)
        {
            Name = name;
            Symbol = symbol;
        }
    }
}
