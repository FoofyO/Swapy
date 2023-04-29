namespace Swapy.DAL.Entities
{
    public class ItemAttribute
    {
        public Guid Id { get; set; }
        public bool IsNew { get; set; }
        public Guid ItemTypeId { get; set; }
        public Subcategory ItemType { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public ItemAttribute() { }

        public ItemAttribute(bool isNew, Guid itemTypeId, Guid productId)
        {
            IsNew = isNew;
            ItemTypeId = itemTypeId;
            ProductId = productId;
        }
    }
}
