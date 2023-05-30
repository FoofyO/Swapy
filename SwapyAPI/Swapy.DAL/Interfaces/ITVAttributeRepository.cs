using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ITVAttributeRepository : IAttributeRepository<TVAttribute>
    {
        Task<TVAttribute> GetByProductIdAsync(string productId);
    }
}
