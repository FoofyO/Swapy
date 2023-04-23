namespace Swapy.DAL.Entities
{
    public class AutoAttribute
    {
        public Guid Id { get; set; }
        public int Miliage { get; set; }
        public int EngineCapacity { get;  set; }
        public DateTime ReleaseYear { get; set; }
        public bool IsNew { get; set; }
        public Guid FuelTypeId { get; set; }
        public FuelType FuelType { get; set; }
        public Guid AutoColorId { get; set; }
        public AutoColor AutoColor { get; set; }
        public Guid TransmissionTypeId { get; set; }
        public TransmissionType TransmissionType { get; set; }
        public Guid AutoBrandTypeId { get; set; }
        public AutoBrandType AutoBrandType { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }

        public AutoAttribute() { }

        public AutoAttribute(int miliage, int engineCapacity, DateTime releaseYear, bool isNew, Guid fuelTypeId, Guid autoColorId, Guid transmissionTypeId, Guid autoBrandTypeId, Guid productId)
        {
            Miliage = miliage;
            EngineCapacity = engineCapacity;
            ReleaseYear = releaseYear;
            IsNew = isNew;
            FuelTypeId = fuelTypeId;
            AutoColorId = autoColorId;
            TransmissionTypeId = transmissionTypeId;
            AutoBrandTypeId = autoBrandTypeId;
            ProductId = productId;
        }
    }
}
