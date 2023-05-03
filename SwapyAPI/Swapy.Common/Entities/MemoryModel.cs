namespace Swapy.Common.Entities
{
    public class MemoryModel
    {
        public Guid Id { get; set; }
        public Guid ModelId { get; set; }
        public Model Model { get; set; }
        public Guid MemoryId { get; set; }
        public Memory Memory { get; set; }
        public ICollection<ElectronicAttribute> ElectronicAttributes { get; set; }

        public MemoryModel() => ElectronicAttributes = new List<ElectronicAttribute>();

        public MemoryModel(Guid modelId, Guid memoryId) : this()
        {
            ModelId = modelId;
            MemoryId = memoryId;
        }
    }
}
