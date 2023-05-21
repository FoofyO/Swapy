namespace Swapy.Common.Entities
{
    public class AutoBrandType
    {
        public string Id { get; set; }
        public string AutoBrandId { get; set; }
        public AutoBrand AutoBrand { get; set; }
        public string AutoTypeId { get; set; }
        public Subcategory AutoType { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; } = new List<AutoAttribute>();

        public AutoBrandType() => Id = Guid.NewGuid().ToString();

        public AutoBrandType(string autoBrandId, string autoTypeId) : this()
        {
            AutoBrandId = autoBrandId;
            AutoTypeId = autoTypeId;
        }
    }
}
