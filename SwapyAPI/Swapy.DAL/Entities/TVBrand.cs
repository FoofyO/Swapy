namespace Swapy.DAL.Entities
{
    public class TVBrand
    {
        public Guid Id { get; set; }
        public string Name { get; set; }  
        public ICollection<TVAttribute> TVAttributes { get; set; }

        public TVBrand() => TVAttributes = new List<TVAttribute>();

        public TVBrand(string name) : this() => Name = name; 
    }
}
 