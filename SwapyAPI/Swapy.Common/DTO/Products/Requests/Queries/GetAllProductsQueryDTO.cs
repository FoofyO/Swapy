using Swapy.Common.Entities;

namespace Swapy.Common.DTO.Products.Requests.Queries
{
    public class GetAllProductsQueryDTO : GetAllProductQueryDTO<Product>
    {
    }

    public class GetAllProductQueryDTO<T>
    {
        public int page { get; set; }
        public int pageSize { get; set; }
        public string title { get; set; }
        public string currencyId { get; set; }
        public decimal? priceMin { get; set; }
        public decimal? priceMax { get; set; }
        public string? categoryId { get; set; }
        public string? subcategoryId { get; set; }
        public string? cityId { get; set; }
        public string? otherUserId { get; set; }
        public bool? sortByPrice { get; set; }
        public bool? reverseSort { get; set; }
    }
}
