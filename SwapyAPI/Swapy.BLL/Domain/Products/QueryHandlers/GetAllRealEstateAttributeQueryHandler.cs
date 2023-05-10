using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllRealEstateAttributeQueryHandler : IRequestHandler<GetAllRealEstateAttributeQuery, IEnumerable<RealEstateAttribute>>
    {
        private readonly string _userId;
        private readonly IRealEstateAttributeRepository _realEstateAttributeRepository;

        public GetAllRealEstateAttributeQueryHandler(string userId, IRealEstateAttributeRepository animalAttributeRepository)
        {
            _userId = userId;
            _realEstateAttributeRepository = animalAttributeRepository;
        }

        public async Task<IEnumerable<RealEstateAttribute>> Handle(GetAllRealEstateAttributeQuery request, CancellationToken cancellationToken)
        {
            var query = await _realEstateAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId == request.CategoryId) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId == request.SubcategoryId) &&
                (request.CityId == null || x.Product.CityId == request.CityId) &&
                (request.UserId == null ? x.Product.UserId != _userId : x.Product.UserId == request.UserId) &&
                (request.AreaMin == null) || (x.Area >= request.AreaMin) &&
                (request.AreaMax == null) || (x.Area <= request.AreaMax) &&
                (request.RoomsMin == null) || (x.Rooms >= request.RoomsMin) &&
                (request.RoomsMax == null) || (x.Rooms <= request.RoomsMax) &&
                (request.IsRent == null || x.IsRent == request.IsRent) &&
                (request.RealEstateTypesId == null || request.RealEstateTypesId.Contains(x.RealEstateTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();

            return result;
        }
    }
}
