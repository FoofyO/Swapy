using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IShopAttributeRepository : IAttributeRepository<ShopAttribute>
    {
        Task IncrementViewsAsync(string shopId);
        Task<ShopAttribute> GetByUserId(string userId);
        Task<bool> FindByShopNameAsync(string shopName);
    }
}
