using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IRealEstateAttributeRepository : IAttributeRepository<RealEstateAttribute>
    {
        Task<RealEstateAttribute> GetByProductIdAsync(string productId);
    }
}
