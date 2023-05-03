namespace Swapy.Common.Entities
{
    public class Color
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelColor> ModelsColors { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; }

        public Color()
        {
            ModelsColors = new List<ModelColor>();
            AutoAttributes = new List<AutoAttribute>();
        }

        public Color(string name) : this() => Name = name;
    }
}
