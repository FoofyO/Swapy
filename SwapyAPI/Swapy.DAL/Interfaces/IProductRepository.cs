using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IProductRepository : IAttributeRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllByUserId(string userId);
        Task IncrementViewsAsync(string id);
        Task<int> GetProductCountForShopAsync(string userId);
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
                                                                          bool? isDisable,
                                                                          bool? sortByPrice,
                                                                          bool? reverseSort);
    }
}
