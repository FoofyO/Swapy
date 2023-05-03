namespace Swapy.Common.Entities
{
    public class ScreenResolution
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; }

        public ScreenResolution() => TVAttributes = new List<TVAttribute>();

        public ScreenResolution(string name) : this() => Name = name;
    }
}
 