using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddElectronicAttributeCommandDTO : AddProductCommandDTO<ElectronicAttribute>
    {
        public bool isNew { get; set; }
        public string memoryModelId { get; set; }
        public string modelColorId { get; set; }
    }
}
