using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.BLL.Domain.Products.QueryHandler
{
    public class GetAllClothesAttributeQueryHandler : IRequestHandler<GetAllClothesAttributeQuery, IEnumerable<ClothesAttribute>>
    {
        private readonly Guid _userId;
        private readonly IClothesAttributeRepository _clothesAttributeRepository;

        public GetAllClothesAttributeQueryHandler(Guid userId, IClothesAttributeRepository clothesAttributeRepository)
        {
            _userId = userId;
            _clothesAttributeRepository = clothesAttributeRepository;
        }

        public async Task<IEnumerable<ClothesAttribute>> Handle(GetAllClothesAttributeQuery request, CancellationToken cancellationToken)
        {
            var query = await _clothesAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId == request.CategoryId) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId == request.SubcategoryId) &&
                (request.CityId == null || x.Product.CityId == request.CityId) &&
                (request.UserId == null ? x.Product.UserId != _userId : x.Product.UserId == request.UserId) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.ClothesSeasonsId == null || request.ClothesSeasonsId.Contains(x.ClothesSeasonId)) &&
                (request.ClothesSizesId == null || request.ClothesSizesId.Contains(x.ClothesSizeId)) &&
                (request.ClothesBrandsId == null || request.ClothesBrandsId.Contains(x.ClothesBrandView.ClothesBrandId)) &&
                (request.ClothesViewsId == null || request.ClothesViewsId.Contains(x.ClothesBrandView.ClothesViewId)) &&
                ((request.ClothesTypesId == null && request.ClothesViewsId != null) || request.ClothesTypesId.Contains(x.ClothesBrandView.ClothesView.ClothesTypeId)) &&
                ((request.ClothesGendersId == null && request.ClothesViewsId != null) || request.ClothesGendersId.Contains(x.ClothesBrandView.ClothesView.GenderId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();

            return result;
        }
    }
}
