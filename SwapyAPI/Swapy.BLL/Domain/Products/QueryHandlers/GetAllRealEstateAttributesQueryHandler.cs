using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllRealEstateAttributesQueryHandler : IRequestHandler<GetAllRealEstateAttributesQuery, ProductsResponseDTO<RealEstateAttribute>>
    {
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public GetAllRealEstateAttributesQueryHandler(IRealEstateAttributeRepository animalAttributeRepository)
        {
            _realEstateAttributeRepository = animalAttributeRepository;
        }

        public async Task<ProductsResponseDTO<RealEstateAttribute>> Handle(GetAllRealEstateAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _realEstateAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.CurrencyId == null || x.Product.CurrencyId.Equals(request.CurrencyId)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId.Equals(request.CategoryId)) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId.Equals(request.SubcategoryId)) &&
                (request.CityId == null || x.Product.CityId.Equals(request.CityId)) &&
                (request.OtherUserId == null ? !x.Product.UserId.Equals(request.UserId) : x.Product.UserId.Equals(request.OtherUserId)) &&
                (request.AreaMin == null) || (x.Area >= request.AreaMin) &&
                (request.AreaMax == null) || (x.Area <= request.AreaMax) &&
                (request.RoomsMin == null) || (x.Rooms >= request.RoomsMin) &&
                (request.RoomsMax == null) || (x.Rooms <= request.RoomsMax) &&
                (request.IsRent == null || x.IsRent == request.IsRent) &&
                (request.RealEstateTypesId == null || request.RealEstateTypesId.Equals(x.RealEstateTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductsResponseDTO<RealEstateAttribute>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
