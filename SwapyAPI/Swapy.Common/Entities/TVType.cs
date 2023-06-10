namespace Swapy.Common.Entities
{
    public class TVType
    {
        public string Id { get; set; }
        public ICollection<TVAttribute> TVAttributes { get; set; } = new List<TVAttribute>(); 
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>(); 
        
        public TVType() => Id = Guid.NewGuid().ToString();
    }
}
