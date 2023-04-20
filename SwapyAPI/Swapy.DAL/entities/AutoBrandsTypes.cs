using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class AutoBrandsTypes
    {
        public Guid Id { get; set; }
        public AutoBrands AutoBrand { get; set; }
        public AutoTypes AutoType { get; set; }
        public List<AutoAttributes> AutoAttributes { get; set; }
    }
}
