using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class AddElectronicAttributeCommand : AddProductCommand<ElectronicAttribute>
    {
        public bool IsNew { get; set; }
        public string MemoryModelId { get; set; }
        public string ModelColorId { get; set; }
    }
}
