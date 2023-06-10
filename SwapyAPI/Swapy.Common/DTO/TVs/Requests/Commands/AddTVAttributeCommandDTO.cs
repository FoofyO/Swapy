﻿using Swapy.Common.DTO.Products.Requests.Commands;
using Swapy.Common.Entities;

namespace Swapy.Common.DTO.TVs.Requests.Commands
{
    public class AddTVAttributeCommandDTO : AddProductCommandDTO
    {
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public string TvTypeId { get; set; }
        public string TvBrandId { get; set; }
        public string ScreenResolutionId { get; set; }
        public string ScreenDiagonalId { get; set; }
    }
}
