namespace Swapy.Common.Entities
{
    public class FuelType
    {
        public string Id { get; set; }
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>();
        public ICollection<AutoAttribute> AutoAttributes { get; set; } = new List<AutoAttribute>();

        public FuelType() => Id = Guid.NewGuid().ToString();
    }
}
