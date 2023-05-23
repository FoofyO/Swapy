using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllTVAttributesQueryHandler : IRequestHandler<GetAllTVAttributesQuery, ProductResponseDTO<TVAttribute>>
    {
        private readonly ITVAttributeRepository _tvAttributeRepository;

        public GetAllTVAttributesQueryHandler(ITVAttributeRepository tvAttributeRepository)
        {
            _tvAttributeRepository = tvAttributeRepository;
        }

        public async Task<ProductResponseDTO<TVAttribute>> Handle(GetAllTVAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _tvAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

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
                (request.IsSmart == null || x.IsSmart == request.IsSmart) &&
                (request.TVTypesId == null || request.TVTypesId.Equals(x.TVTypeId)) &&
                (request.TVBrandsId == null || request.TVBrandsId.Equals(x.TVBrandId)) &&
                (request.ScreenResolutionsId == null || request.ScreenResolutionsId.Equals(x.ScreenResolutionId)) &&
                (request.ScreenDiagonalsId == null || request.ScreenDiagonalsId.Equals(x.ScreenDiagonalId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductResponseDTO<TVAttribute>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
