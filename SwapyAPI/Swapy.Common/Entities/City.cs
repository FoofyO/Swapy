namespace Swapy.Common.Entities
{
    public class City
    {
        public string Id { get; set; }
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>();

        public City() => Id = Guid.NewGuid().ToString();
    } 
}
