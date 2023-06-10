using MediatR;
using Swapy.BLL.Domain.Autos.Queries;
using Swapy.Common.DTO.Products.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Autos.QueryHandlers
{
    public class GetAllAutoAttributesQueryHandler : IRequestHandler<GetAllAutoAttributesQuery, ProductsResponseDTO<ProductResponseDTO>>
    {
        private readonly IAutoAttributeRepository _autoAttributeRepository;

        public GetAllAutoAttributesQueryHandler(IAutoAttributeRepository autoAttributeRepository, IFavoriteProductRepository favoriteProductRepository) => _autoAttributeRepository = autoAttributeRepository;

        public async Task<ProductsResponseDTO<ProductResponseDTO>> Handle(GetAllAutoAttributesQuery request, CancellationToken cancellationToken)
        {
            return await _autoAttributeRepository.GetAllFilteredAsync(request.Page,
                                                                      request.PageSize,
                                                                      request.UserId,
                                                                      request.Title,
                                                                      request.CurrencyId,
                                                                      request.PriceMin,
                                                                      request.PriceMax,
                                                                      request.CategoryId,
                                                                      request.SubcategoryId,
                                                                      request.CityId,
                                                                      request.OtherUserId,
                                                                      request.IsNew,
                                                                      request.MiliageMin,
                                                                      request.MiliageMax,
                                                                      request.EngineCapacityMin,
                                                                      request.EngineCapacityMax,
                                                                      request.ReleaseYearOlder,
                                                                      request.ReleaseYearNewer,
                                                                      request.FuelTypesId,
                                                                      request.AutoColorsId,
                                                                      request.TransmissionTypesId,
                                                                      request.AutoBrandsId,
                                                                      request.AutoTypesId,
                                                                      request.SortByPrice,
                                                                      request.ReverseSort,
                                                                      request.Language);
        }
    }
}
