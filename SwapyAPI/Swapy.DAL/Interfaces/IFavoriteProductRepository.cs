using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IFavoriteProductRepository : IAttributeRepository<FavoriteProduct>
    {
        Task<FavoriteProduct> GetByProductAndUserIdAsync(string productId, string userId);
    }
}
