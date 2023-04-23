namespace Swapy.DAL.Entities
{
    public class ElectronicAttribute
    {
        public Guid Id { get; set; }
        public bool IsNew { get; set; }
        public Guid MemoryModelId { get; set; }
        public MemoryModel MemoryModel { get; set; }
        public Guid ModelColorId { get; set; }
        public ModelColor ModelColor { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public ElectronicAttribute() { }

        public ElectronicAttribute(bool isNew, Guid memoryModelId, Guid modelColorId, Guid productId)
        {
            IsNew = isNew;
            MemoryModelId = memoryModelId;
            ModelColorId = modelColorId;
            ProductId = productId;
        }
    }
}
