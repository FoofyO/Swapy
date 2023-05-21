namespace Swapy.Common.Entities
{
    public class ClothesSize
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool IsShoe { get; set; }
        public bool IsChild { get; set; }
        public string Size { get; set; }
        public ICollection<ClothesAttribute> ClothesAttributes { get; set; } = new List<ClothesAttribute>();

        public ClothesSize() => Id = Guid.NewGuid().ToString();

        public ClothesSize(string name, bool isShoe, bool isChild, string size) : this()
        {
            Name = name;
            IsShoe = isShoe;
            IsChild = isChild;
            Size = size;
        }
    } 
} 
  