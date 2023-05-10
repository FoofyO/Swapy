using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateElectronicAttributeCommand : UpdateProductCommand
    {
        public string ElectronicAttributeId { get; set; }
        public bool IsNew { get; set; }
        public string MemoryModelId { get; set; }
        public string ModelColorId { get; set; }
    }
}
