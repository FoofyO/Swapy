namespace Swapy.Common.Entities
{
    public class TransmissionType
    {
        public string Id { get; set; }
        public ICollection<LocalizationValue> Names { get; set; } = new List<LocalizationValue>(); public ICollection<AutoAttribute> AutoAttributes { get; set; } = new List<AutoAttribute>(); 

        public TransmissionType() => Id = Guid.NewGuid().ToString();
    }
}
