namespace Swapy.Common.Entities
{
    public class ScreenDiagonal
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; }

        public ScreenDiagonal() => TVAttributes = new List<TVAttribute>();

        public ScreenDiagonal(string name) : this() => Name = name;
    }
}
