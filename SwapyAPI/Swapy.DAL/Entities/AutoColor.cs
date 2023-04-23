namespace Swapy.DAL.Entities
{
    public class AutoColor
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; }

        public AutoColor() => AutoAttributes = new List<AutoAttribute>();

        public AutoColor(string name) : this() => Name = name;
    }
}
