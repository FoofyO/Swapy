using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class Memories
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<MemoriesModels> MemoriesModels { get; set; }
    }
}
