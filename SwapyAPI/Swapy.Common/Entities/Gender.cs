namespace Swapy.Common.Entities
{
    public class Gender
    {
        public string Id { get; set; }
        public ICollection<ClothesView> ClothesViews { get; set; } = new List<ClothesView>();
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>();

        public Gender() => Id = Guid.NewGuid().ToString();
    }
}
 