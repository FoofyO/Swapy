using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllTVAttributesQueryHandler : IRequestHandler<GetAllTVAttributesQuery, ProductResponseDTO<TVAttribute>>
    {
        private readonly string _userId;
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public GetAllTVAttributesQueryHandler(string userId, ITVAttributeRepository tvAttributeRepository)
        {
            _userId = userId;
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<ProductResponseDTO<TVAttribute>> Handle(GetAllTVAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _tvAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId == request.CategoryId) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId == request.SubcategoryId) &&
                (request.CityId == null || x.Product.CityId == request.CityId) &&
                (request.UserId == null ? x.Product.UserId != _userId : x.Product.UserId == request.UserId) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.IsSmart == null || x.IsSmart == request.IsSmart) &&
                (request.TVTypesId == null || request.TVTypesId.Contains(x.TVTypeId)) &&
                (request.TVBrandsId == null || request.TVBrandsId.Contains(x.TVBrandId)) &&
                (request.ScreenResolutionsId == null || request.ScreenResolutionsId.Contains(x.ScreenResolutionId)) &&
                (request.ScreenDiagonalsId == null || request.ScreenDiagonalsId.Contains(x.ScreenDiagonalId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductResponseDTO<TVAttribute>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
