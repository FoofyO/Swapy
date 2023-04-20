using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class AutoAttributes
    {
        public Guid Id { get; set; }
        public int Miliage { get; set; }
        public int EngineCapacity { get;  set; }
        public DateTime ReleaseYear { get; set; }
        public bool IsNew { get; set; }
        public FuelTypes FuelType { get; set; }
        public AutoColors AutoColor { get; set; }
        public TransmissionTypes TransmissionType { get; set; }
        public AutoBrandsTypes AutoBrandType { get; set; }
        ///Product Product { get; set; }
    }
}
