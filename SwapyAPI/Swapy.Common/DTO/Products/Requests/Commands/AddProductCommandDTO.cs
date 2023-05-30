﻿namespace Swapy.Common.DTO.Products.Requests.Commands
{
    public abstract class AddProductCommandDTO<T>
    {
        public string title { get; set; }
        public string description { get; set; }
        public decimal price { get; set; }
        public string currencyId { get; set; }
        public string categoryId { get; set; }
        public string subcategoryId { get; set; }
        public string cityId { get; set; }
    }
}
