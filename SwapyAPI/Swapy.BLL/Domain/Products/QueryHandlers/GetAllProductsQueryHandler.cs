using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductResponseDTO<Product>>
    {
        private readonly IProductRepository _productRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public async Task<ProductResponseDTO<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var query = await _productRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Title.Contains(request.Title)) &&
                (request.CurrencyId == null || x.CurrencyId.Equals(request.CurrencyId)) &&
                (request.PriceMin == null) || (x.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.CategoryId.Equals(request.CategoryId)) &&
                (request.SubcategoryId == null || x.SubcategoryId.Equals(request.SubcategoryId)) &&
                (request.CityId == null || x.CityId.Equals(request.CityId)) &&
                (request.OtherUserId == null ? !x.UserId.Equals(request.UserId) : x.UserId.Equals(request.OtherUserId)));
            if (request.SortByPrice == true) query.OrderBy(x => x.Price);
            else query.OrderBy(x => x.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductResponseDTO<Product>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
