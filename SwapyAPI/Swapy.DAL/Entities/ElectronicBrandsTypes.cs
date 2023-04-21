using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class ElectronicBrandsTypes
    {
        public Guid Id { get; set; }
        public ElectronicBrands ElectronicBrand { get; set; }
        public ElectronicTypes ElectronicType { get; set; }
        public List<Models> Models { get; set; }
    }
}
