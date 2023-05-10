namespace Swapy.Common.Entities
{
    public class TVType
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>();

        public TVType() { }

        public TVType(string name) => Name = name;
    }
}
