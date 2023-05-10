﻿namespace Swapy.BLL.Domain.Products.Commands
{
    public class UpdateClothesAttributeCommand : UpdateProductCommand
    {
        public string ClothesAttributeId { get; set; }
        public bool IsNew { get; set; }
        public string ClothesSeasonId { get; set; }
        public string ClothesSizeId { get; set; }
        public string ClothesBrandViewId { get; set; }
    }
}
