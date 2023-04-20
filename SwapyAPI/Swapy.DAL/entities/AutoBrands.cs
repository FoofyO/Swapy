﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Entities
{
    public class AutoBrands
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<AutoBrandsTypes> AutoBrandsTypes { get; set; }
    }
}
