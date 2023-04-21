using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class ModelsColors
    {
        public Guid Id { get; set; }
        public Models Model { get; set; }
        public Colors Color { get; set; }
        public List<ElectronicAttributes> ElectronicAttributes { get; set; }
    }
}
