namespace Swapy.Common.Entities
{
    public class Color
    {
        public string Id { get; set; }
        public ICollection<ModelColor> ModelsColors { get; set; } = new List<ModelColor>();
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>();
        public ICollection<AutoAttribute> AutoAttributes { get; set; } = new List<AutoAttribute>();

        public Color() => Id = Guid.NewGuid().ToString();
    }
}
