namespace Swapy.DAL.Entities
{
    public class ElectronicBrandType
    {
        public Guid Id { get; set; }
        public Guid ElectronicBrandId { get; set; }
        public ElectronicBrand ElectronicBrand { get; set; }
        public Guid ElectronicTypeId { get; set; }
        public ElectronicType ElectronicType { get; set; }
        public ICollection<Model> Models { get; set; }

        public ElectronicBrandType() => Models = new List<Model>();

        public ElectronicBrandType(Guid electronicBrandId, Guid electronicTypeId) : this()
        {
            ElectronicBrandId = electronicBrandId;
            ElectronicTypeId = electronicTypeId;
        }
    }
}
