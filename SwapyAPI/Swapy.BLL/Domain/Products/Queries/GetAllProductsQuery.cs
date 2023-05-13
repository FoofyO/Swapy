using MediatR;
using Swapy.Common.DTO;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllProductsQuery : GetAllProductQuery<Product>
    {
    }

    public class GetAllProductQuery<T> : IRequest<ProductResponseDTO<T>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public string? CategoryId { get; set; }
        public string? SubcategoryId { get; set; }
        public string? CityId { get; set; }
        public string? UserId { get; set; }
        public bool? SortByPrice { get; set; }
        public bool? ReverseSort { get; set; }
    }
}
