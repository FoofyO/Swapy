using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IItemAttributeRepository : IAttributeRepository<ItemAttribute>
    {
        Task<ItemAttribute> GetByProductIdAsync(string productId);
    }
}
