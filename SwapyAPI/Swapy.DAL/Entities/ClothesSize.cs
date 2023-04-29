namespace Swapy.DAL.Entities
{
    public class ClothesSize
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsShoe { get; set; }
        public bool IsChild { get; set; }
        public string Size { get; set; }
        public ICollection<ClothesAttribute> ClothesAttributes { get; set; }
          
        public ClothesSize() => ClothesAttributes = new List<ClothesAttribute>();

        public ClothesSize(string name, bool isShoe, bool isChild, string size) : this() {
            Name = name;
            IsShoe = isShoe;
            IsChild = isChild;
            Size = size;
        }
    } 
} 
  