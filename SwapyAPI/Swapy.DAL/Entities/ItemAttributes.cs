using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class ItemAttributes
    {
        public Guid Id { get; set; }
        public bool IsNew { get; set; }
        public ItemTypes ItemType { get; set; }
        ///Product Product { get; set; }
    }
}
