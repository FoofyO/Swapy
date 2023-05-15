using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllClothesAttributesQueryHandler : IRequestHandler<GetAllClothesAttributesQuery, ProductResponseDTO<ClothesAttribute>>
    {
        private readonly string _userId;
        private readonly IClothesAttributeRepository _clothesAttributeRepository;

        public GetAllClothesAttributesQueryHandler(string userId, IClothesAttributeRepository clothesAttributeRepository)
        {
            _userId = userId;
            _clothesAttributeRepository = clothesAttributeRepository;
        }

        public async Task<ProductResponseDTO<ClothesAttribute>> Handle(GetAllClothesAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _clothesAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId.Equals(request.CategoryId)) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId.Equals(request.SubcategoryId)) &&
                (request.CityId == null || x.Product.CityId.Equals(request.CityId)) &&
                (request.UserId == null ? !x.Product.UserId.Equals(_userId) : x.Product.UserId.Equals(request.UserId)) &&
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
            var result = await query.ToListAsync();
            return new ProductResponseDTO<ClothesAttribute>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
