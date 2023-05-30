using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAutoAttributeRepository : IAttributeRepository<AutoAttribute>
    {
        Task<AutoAttribute> GetByProductIdAsync(string productId);
    }
}
