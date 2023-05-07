using Azure.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQuery, IEnumerable<Product>>
    {
        private readonly Guid _userId;
        private readonly IProductRepository _productRepository;

        public GetAllProductQueryHandler(Guid userId, IProductRepository productRepository)
        {
            _userId = userId;
            _productRepository = productRepository;
        }
        
        public async Task<IEnumerable<Product>> Handle(GetAllProductQuery request, CancellationToken cancellationToken)
        {
            var query = await _productRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.CategoryId == request.CategoryId) &&
                (request.SubcategoryId == null || x.SubcategoryId == request.SubcategoryId) &&
                (request.CityId == null || x.CityId == request.CityId) &&
                (request.UserId == null ? x.UserId != _userId : x.UserId == request.UserId));
            if (request.SortByPrice == true) query.OrderBy(x => x.Price);
            else query.OrderBy(x => x.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();

            return result;
        }
    }
}
