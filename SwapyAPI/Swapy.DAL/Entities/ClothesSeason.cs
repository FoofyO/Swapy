namespace Swapy.DAL.Entities
{
    public class ClothesSeason
    {
        public Guid Id { get; set; }
        public string Name { get; set; }   
        public ICollection<ClothesAttribute> ClothesAttributes { get; set; }

        public ClothesSeason() {
            ClothesAttributes = new List<ClothesSeason>();
        }

        public ClothesSeason(string name) : this()
        {
            Name = name;

        }
             
    }
}     