using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateElectronicAttributeCommand : UpdateProductCommand
    {
        public Guid ElectronicAttributeId { get; set; }
        public bool IsNew { get; set; }
        public Guid MemoryModelId { get; set; }
        public Guid ModelColorId { get; set; }
    }
}
