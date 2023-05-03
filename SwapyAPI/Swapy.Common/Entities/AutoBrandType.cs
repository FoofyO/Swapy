namespace Swapy.Common.Entities
{
    public class AutoBrandType
    {
        public Guid Id { get; set; }
        public Guid AutoBrandId { get; set; }
        public AutoBrand AutoBrand { get; set; }
        public Guid AutoTypeId { get; set; }
        public Subcategory AutoType { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; }

        public AutoBrandType() => AutoAttributes = new List<AutoAttribute>();

        public AutoBrandType(Guid autoBrandId, Guid autoTypeId) : this()
        {
            AutoBrandId = autoBrandId;
            AutoTypeId = autoTypeId;
        }
    }
}
