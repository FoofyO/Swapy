using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllByUserId(Guid userId);
    }
}
