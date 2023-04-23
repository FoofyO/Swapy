namespace Swapy.DAL.Entities
{
    public class ModelColor
    {
        public Guid Id { get; set; }
        public Guid ModelId { get; set; }
        public Model Model { get; set; }
        public Guid ColorId { get; set; }
        public Color Color { get; set; }
        public ICollection<ElectronicAttribute> ElectronicAttributes { get; set; }

        public ModelColor() => ElectronicAttributes = new List<ElectronicAttribute>();

        public ModelColor(Guid id, Guid modelId, Guid colorId) : this()
        {
            Id = id;
            ModelId = modelId;
            ColorId = colorId;
        }
    }
}
