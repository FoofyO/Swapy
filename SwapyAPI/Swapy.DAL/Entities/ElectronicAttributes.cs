using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class ElectronicAttributes
    {
        public Guid Id { get; set; }
        public bool IsNew { get; set; }
        public MemoriesModels MemoryModel { get; set; }
        public ModelsColors ModelColor { get; set; }
        ///Product Product { get; set; }
    }
}
