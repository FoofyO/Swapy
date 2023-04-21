using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class Models
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ElectronicBrandsTypes ElectronicBrandType { get; set; }
        public List<MemoriesModels> MemoriesModels { get; set; }
        public List<ModelsColors> ModelsColors { get; set; }
    }
}
