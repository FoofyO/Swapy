﻿namespace Swapy.Common.Entities
{
    public class RealEstateAttribute
    {
        public string Id { get; set; }
        public int Area { get; set; }
        public int Rooms { get;  set; }
        public bool IsRent{ get; set; } 
        public string RealEstateTypeId { get; set; }
        public Subcategory RealEstateType { get; set; }
        public string ProductId { get; set; }
        public Product Product { get; set; }

        public RealEstateAttribute() { }

        public RealEstateAttribute(int area, int rooms, bool isRent, string realEstateTypeId, string productId)
        {
            Area = area;
            Rooms = rooms; 
            IsRent = isRent;
            ProductId = productId;
            RealEstateTypeId = realEstateTypeId;
        }
    }
} 
