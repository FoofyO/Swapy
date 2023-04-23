namespace Swapy.DAL.Entities
{
    public class TransmissionType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<AutoAttribute> AutoAttributes { get; set; }

        public TransmissionType() => AutoAttributes = new List<AutoAttribute>();

        public TransmissionType(string name) : this() => Name = name;
    }
}
