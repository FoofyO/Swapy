using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.BLL.Services;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetAllProductsQueryHandler(IProductRepository productRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _productRepository = productRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }
        
        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
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

            FavoriteProductsService favoriteProductsService = new(_favoriteProductRepository);

            var result = await query.Select(x => new ProductResponseDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                City = x.City.Name,
                Currency = x.Currency.Name,
                CurrencySymbol = x.Currency.Symbol,
                DateTime = x.DateTime,
                Images = x.Images.Select(i => i.Image).ToList(),
                UserType = x.User.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await favoriteProductsService.IsFavoriteAsync(item.Id, request.UserId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
