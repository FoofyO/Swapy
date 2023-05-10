namespace Swapy.Common.Entities
{
    public class ModelColor
    {
        public string Id { get; set; }
        public string ModelId { get; set; }
        public Model Model { get; set; }
        public string ColorId { get; set; }
        public Color Color { get; set; }
        public ICollection<ElectronicAttribute> ElectronicAttributes { get; set; } = new List<ElectronicAttribute>();

        public ModelColor() { }

        public ModelColor(string id, string modelId, string colorId)
        {
            Id = id;
            ModelId = modelId;
            ColorId = colorId;
        }
    }
}
