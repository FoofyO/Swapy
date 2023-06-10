using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Interfaces
{
    public interface IAutoAttributeRepository : IAttributeRepository<AutoAttribute>
    {
        Task<AutoAttribute> GetByProductIdAsync(string productId);
        Task<ProductsResponseDTO<ProductResponseDTO>> GetAllFilteredAsync(int page,
                                                                               int pageSize,
                                                                               string userId,
                                                                               string title,
                                                                               string currencyId,
                                                                               decimal? priceMin,
                                                                               decimal? priceMax,
                                                                               string categoryId,
                                                                               string subcategoryId,
                                                                               string cityId,
                                                                               string otherUserId,
                                                                               bool? isNew,
                                                                               int? miliageMin,
                                                                               int? miliageMax,
                                                                               int? engineCapacityMin,
                                                                               int? engineCapacityMax,
                                                                               DateTime? releaseYearOlder,
                                                                               DateTime? releaseYearNewer,
                                                                               List<string> fuelTypesId,
                                                                               List<string> autoColorsId,
                                                                               List<string> transmissionTypesId,
                                                                               List<string> autoBrandsId,
                                                                               List<string> autoTypesId,
                                                                               bool? sortByPrice,
                                                                               bool? reverseSort,
                                                                               Languages language);
    }
}
