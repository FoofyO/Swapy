namespace Swapy.Common.Entities
{
    public class ScreenDiagonal
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>();

        public ScreenDiagonal() { }

        public ScreenDiagonal(string name) => Name = name;
    }
}
