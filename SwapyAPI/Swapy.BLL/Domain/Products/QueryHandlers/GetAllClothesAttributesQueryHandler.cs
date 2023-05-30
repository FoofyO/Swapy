using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.BLL.Services;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesAttributesQueryHandler : IRequestHandler<GetAllClothesAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IClothesAttributeRepository _clothesAttributeRepository;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public GetAllClothesAttributesQueryHandler(IClothesAttributeRepository clothesAttributeRepository, IFavoriteProductRepository favoriteProductRepository)
        {
            _clothesAttributeRepository = clothesAttributeRepository;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllClothesAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _clothesAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.CurrencyId == null || x.Product.CurrencyId.Equals(request.CurrencyId)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId.Equals(request.CategoryId)) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId.Equals(request.SubcategoryId)) &&
                (request.CityId == null || x.Product.CityId.Equals(request.CityId)) &&
                (request.OtherUserId == null ? !x.Product.UserId.Equals(request.UserId) : x.Product.UserId.Equals(request.OtherUserId)) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.ClothesSeasonsId == null || request.ClothesSeasonsId.Equals(x.ClothesSeasonId)) &&
                (request.ClothesSizesId == null || request.ClothesSizesId.Equals(x.ClothesSizeId)) &&
                (request.ClothesBrandsId == null || request.ClothesBrandsId.Equals(x.ClothesBrandView.ClothesBrandId)) &&
                (request.ClothesViewsId == null || request.ClothesViewsId.Equals(x.ClothesBrandView.ClothesViewId)) &&
                ((request.ClothesTypesId == null && request.ClothesViewsId != null) || request.ClothesTypesId.Equals(x.ClothesBrandView.ClothesView.ClothesTypeId)) &&
                ((request.ClothesGendersId == null && request.ClothesViewsId != null) || request.ClothesGendersId.Equals(x.ClothesBrandView.ClothesView.GenderId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();

            FavoriteProductsService favoriteProductsService = new(_favoriteProductRepository);

            var result = await query.Select(x => new ProductResponseDTO()
            {
                Id = x.ProductId,
                Title = x.Product.Title,
                Price = x.Product.Price,
                City = x.Product.City.Name,
                Currency = x.Product.Currency.Name,
                CurrencySymbol = x.Product.Currency.Symbol,
                DateTime = x.Product.DateTime,
                Images = x.Product.Images.Select(i => i.Image).ToList(),
                UserType = x.Product.User.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await favoriteProductsService.IsFavoriteAsync(item.Id, request.UserId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
