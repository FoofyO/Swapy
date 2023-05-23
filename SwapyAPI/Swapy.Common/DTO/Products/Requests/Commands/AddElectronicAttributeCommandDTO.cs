using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public class AddElectronicAttributeCommandDTO : AddProductCommandDTO<ElectronicAttribute>
    {
        public bool IsNew { get; set; }
        public string MemoryModelId { get; set; }
        public string ModelColorId { get; set; }
    }
}
