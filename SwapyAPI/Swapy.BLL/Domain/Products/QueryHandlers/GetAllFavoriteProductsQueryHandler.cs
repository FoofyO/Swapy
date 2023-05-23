using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllFavoriteProductsQueryHandler : IRequestHandler<GetAllFavoriteProductsQuery, ProductResponseDTO<FavoriteProduct>>
    {
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetAllFavoriteProductsQueryHandler(IFavoriteProductRepository favoriteProductRepository)
        {
            _favoriteProductRepository = favoriteProductRepository;
        }
        
        public async Task<ProductResponseDTO<FavoriteProduct>> Handle(GetAllFavoriteProductsQuery request, CancellationToken cancellationToken)
        {
            if ((request.ProductId == null) == (request.UserId == null)) throw new ArgumentException("Specify one ID for either the product or the user.");

            var query = await _favoriteProductRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.CurrencyId == null || x.Product.CurrencyId.Equals(request.CurrencyId)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId.Equals(request.CategoryId)) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId.Equals(request.SubcategoryId)) &&
                (request.CityId == null || x.Product.CityId.Equals(request.CityId)) &&
                (request.UserId == null || x.Product.UserId.Equals(request.UserId)) &&
                (request.OtherUserId == null || x.Product.UserId.Equals(request.OtherUserId)) &&
                (request.ProductId == null || x.ProductId.Equals(request.ProductId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductResponseDTO<FavoriteProduct>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
