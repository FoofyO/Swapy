using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class FuelTypes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AutoAttributes> AutoAttributes { get; set; }
    }
}
