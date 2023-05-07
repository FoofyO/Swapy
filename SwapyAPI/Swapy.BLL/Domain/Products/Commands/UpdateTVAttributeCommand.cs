﻿namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateTVAttributeCommand : UpdateProductCommand
    {
        public Guid TVAttributeId { get; set; }
        public bool IsNew { get; set; }
        public bool IsSmart { get; set; }
        public Guid TVTypeId { get; set; }
        public Guid TVBrandId { get; set; }
        public Guid ScreenResolutionId { get; set; }
        public Guid ScreenDiagonalId { get; set; }
    }
}
