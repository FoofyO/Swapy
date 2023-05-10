namespace Swapy.Common.Entities
{
    public class ClothesSeason
    {
        public string Id { get; set; }
        public string Name { get; set; }   
        public ICollection<ClothesAttribute> ClothesAttributes { get; set; } = new List<ClothesAttribute>();

        public ClothesSeason() { }

        public ClothesSeason(string name) => Name = name;
    }
}     