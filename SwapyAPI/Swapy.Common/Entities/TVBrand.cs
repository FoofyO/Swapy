namespace Swapy.Common.Entities
{
    public class TVBrand
    {
        public string Id { get; set; }
        public string Name { get; set; }  
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>();

        public TVBrand() { }

        public TVBrand(string name) => Name = name; 
    }
}
 