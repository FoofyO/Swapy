using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddAutoAttributeCommandDTO : AddProductCommandDTO<AutoAttribute>
    {
        public int miliage { get; set; }
        public int engineCapacity { get; set; }
        public DateTime releaseYear { get; set; }
        public bool isNew { get; set; }
        public string fuelTypeId { get; set; }
        public string autoColorId { get; set; }
        public string transmissionTypeId { get; set; }
        public string autoModelId { get; set; }
    }
}
