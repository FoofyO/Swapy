using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IShopAttributeRepository : IAttributeRepository<ShopAttribute>
    {
        Task IncrementViewsAsync(string shopId);
        Task<bool> FindByShopNameAsync(string shopName);
    }
}
