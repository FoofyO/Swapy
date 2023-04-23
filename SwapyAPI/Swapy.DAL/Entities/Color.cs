namespace Swapy.DAL.Entities
{
    public class Color
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ModelColor> ModelsColors { get; set; }

        public Color() => ModelsColors = new List<ModelColor>();

        public Color(string name) : this() => Name = name;
    }
}
