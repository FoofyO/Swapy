using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAutoAttributeQueryHandler : IRequestHandler<GetAllAutoAttributeQuery, IEnumerable<AutoAttribute>>
    {
        private readonly string _userId;
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public GetAllAutoAttributeQueryHandler(string userId, IAutoAttributeRepository autoAttributeRepository)
        {
            _userId = userId;
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<IEnumerable<AutoAttribute>> Handle(GetAllAutoAttributeQuery request, CancellationToken cancellationToken)
        {
            var query = await _autoAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId == request.CategoryId) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId == request.SubcategoryId) &&
                (request.CityId == null || x.Product.CityId == request.CityId) &&
                (request.UserId == null ? x.Product.UserId != _userId : x.Product.UserId == request.UserId) &&
                (request.MiliageMin == null) || (x.Miliage >= request.MiliageMin) &&
                (request.MiliageMax == null) || (x.Miliage <= request.MiliageMax) &&
                (request.EngineCapacityMin == null) || (x.EngineCapacity >= request.EngineCapacityMin) &&
                (request.EngineCapacityMax == null) || (x.EngineCapacity <= request.EngineCapacityMax) &&
                (request.ReleaseYearOlder == null) || (x.ReleaseYear >= request.ReleaseYearOlder) &&
                (request.ReleaseYearNewer == null) || (x.ReleaseYear <= request.ReleaseYearNewer) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.FuelTypesId == null || request.FuelTypesId.Contains(x.FuelTypeId)) &&
                (request.AutoColorsId == null || request.AutoColorsId.Contains(x.AutoColorId)) &&
                (request.TransmissionTypesId == null || request.TransmissionTypesId.Contains(x.TransmissionTypeId)) &&
                (request.AutoBrandsId == null || request.AutoBrandsId.Contains(x.AutoBrandType.AutoBrandId)) &&
                (request.AutoTypesId == null || request.AutoTypesId.Contains(x.AutoBrandType.AutoTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();

            return result;
        }
    }
}
