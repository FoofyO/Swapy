namespace Swapy.Common.Entities
{
    public class ScreenDiagonal
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>();

        public ScreenDiagonal() => Id = Guid.NewGuid().ToString();

        public ScreenDiagonal(string name) : this() => Name = name;
    }
}
