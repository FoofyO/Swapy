namespace Swapy.Common.Entities
{
    public class ClothesSeason
    {
        public string Id { get; set; }
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>();
        public ICollection<ClothesAttribute> ClothesAttributes { get; set; } = new List<ClothesAttribute>();

        public ClothesSeason() => Id = Guid.NewGuid().ToString();
    }
}     