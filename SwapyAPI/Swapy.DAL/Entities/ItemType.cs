namespace Swapy.DAL.Entities
{
    public class ItemType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ItemAttribute> ItemAttributes { get; set; }
        public Guid SubcategoryId { get; set; }
        public Subcategory Subcategory { get; set; }

        public ItemType() => ItemAttributes = new List<ItemAttribute>();

        public ItemType(string name, Guid subcategoryId) : this()
        {
            Name = name;
            SubcategoryId = subcategoryId;
        }
    }
}
