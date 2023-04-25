namespace Swapy.DAL.Entities
{
    public class TVType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; }

        public TVType() => TVAttributes = new List<TVAttribute>();

        public TVType(string name) : this() => Name = name;
    }
}
