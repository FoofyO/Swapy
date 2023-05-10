namespace Swapy.Common.Entities
{
    public class ScreenResolution
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>();

        public ScreenResolution() { }

        public ScreenResolution(string name) => Name = name;
    }
}
 