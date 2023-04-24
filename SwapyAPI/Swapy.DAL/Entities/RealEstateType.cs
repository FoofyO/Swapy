namespace Swapy.DAL.Entities
{
    public class RealEstateType
    { 
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<RealEstateAttribute> RealEstateAttributes{ get; set; }
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public RealEstateType() => RealEstateAttributes= new List<RealEstateAttribute>();

        public RealEstateType(string name, Guid subcategoryId) : this()
        {
            Name = name;
            SubcategoryId = subcategoryId;
        }
    }
}
