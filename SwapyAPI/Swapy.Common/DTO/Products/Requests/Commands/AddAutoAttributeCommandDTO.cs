using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddAutoAttributeCommandDTO : AddProductCommandDTO<AutoAttribute>
    {
        public int Miliage { get; set; }
        public int EngineCapacity { get; set; }
        public DateTime ReleaseYear { get; set; }
        public bool IsNew { get; set; }
        public string FuelTypeId { get; set; }
        public string AutoColorId { get; set; }
        public string TransmissionTypeId { get; set; }
        public string AutoModelId { get; set; }
    }
}
