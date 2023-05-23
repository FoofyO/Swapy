using MediatR;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Products.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Products.QueryHandlers
{
    public class GetAllAutoAttributesQueryHandler : IRequestHandler<GetAllAutoAttributesQuery, ProductResponseDTO<AutoAttribute>>
    {
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public GetAllAutoAttributesQueryHandler(IAutoAttributeRepository autoAttributeRepository)
        {
            _autoAttributeRepository = autoAttributeRepository;
        }

        public async Task<ProductResponseDTO<AutoAttribute>> Handle(GetAllAutoAttributesQuery request, CancellationToken cancellationToken)
        {
            var query = await _autoAttributeRepository.GetByPageAsync(request.Page, request.PageSize);

            query = query.Where(x =>
                (request.Title == null || x.Product.Title.Contains(request.Title)) &&
                (request.CurrencyId == null || x.Product.CurrencyId.Equals(request.CurrencyId)) &&
                (request.PriceMin == null) || (x.Product.Price >= request.PriceMin) &&
                (request.PriceMax == null) || (x.Product.Price <= request.PriceMax) &&
                (request.CategoryId == null || x.Product.CategoryId.Equals(request.CategoryId)) &&
                (request.SubcategoryId == null || x.Product.SubcategoryId.Equals(request.SubcategoryId)) &&
                (request.CityId == null || x.Product.CityId.Equals(request.CityId)) &&
                (request.OtherUserId == null ? !x.Product.UserId.Equals(request.UserId) : x.Product.UserId.Equals(request.OtherUserId)) &&
                (request.MiliageMin == null) || (x.Miliage >= request.MiliageMin) &&
                (request.MiliageMax == null) || (x.Miliage <= request.MiliageMax) &&
                (request.EngineCapacityMin == null) || (x.EngineCapacity >= request.EngineCapacityMin) &&
                (request.EngineCapacityMax == null) || (x.EngineCapacity <= request.EngineCapacityMax) &&
                (request.ReleaseYearOlder == null) || (x.ReleaseYear >= request.ReleaseYearOlder) &&
                (request.ReleaseYearNewer == null) || (x.ReleaseYear <= request.ReleaseYearNewer) &&
                (request.IsNew == null || x.IsNew == request.IsNew) &&
                (request.FuelTypesId == null || request.FuelTypesId.Equals(x.FuelTypeId)) &&
                (request.AutoColorsId == null || request.AutoColorsId.Equals(x.AutoColorId)) &&
                (request.TransmissionTypesId == null || request.TransmissionTypesId.Equals(x.TransmissionTypeId)) &&
                (request.AutoBrandsId == null || request.AutoBrandsId.Equals(x.AutoModel.AutoBrandId)) &&
                (request.AutoTypesId == null || request.AutoTypesId.Equals(x.AutoModel.AutoTypeId)) );
            if (request.SortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (request.ReverseSort == true) query.Reverse();
            var result = await query.ToListAsync();
            return new ProductResponseDTO<AutoAttribute>(result, query.Count(), (int)Math.Ceiling(Convert.ToDouble(query.Count() / request.PageSize)));
        }
    }
}
