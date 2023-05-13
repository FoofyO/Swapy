using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IProductRepository : IAttributeRepositoryy<Product>
    {
        Task<IEnumerable<Product>> GetAllByUserId(string userId);
        Task IncrementViewsAsync(string id);
        Task<int> GetProductCountForShopAsync(string userId);
    }
}
