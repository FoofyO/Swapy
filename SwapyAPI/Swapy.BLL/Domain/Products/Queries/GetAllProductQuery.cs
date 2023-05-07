using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Products.Queries
{
    public class GetAllProductQuery : GetAllProductQuery<Product>
    {
    }

    public class GetAllProductQuery<T> : IRequest<IEnumerable<T>>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string Title { get; set; }
        public decimal? PriceMin { get; set; }
        public decimal? PriceMax { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? SubcategoryId { get; set; }
        public Guid? CityId { get; set; }
        public Guid? UserId { get; set; }
        public bool? SortByPrice { get; set; }
        public bool? ReverseSort { get; set; }
    }
}
