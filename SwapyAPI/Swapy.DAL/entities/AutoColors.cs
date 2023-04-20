using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class AutoColors
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AutoAttributes> AutoAttributes { get; set; }
    }
}
