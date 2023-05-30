using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAnimalAttributeRepository : IAttributeRepository<AnimalAttribute>
    {
        Task<AnimalAttribute> GetByProductIdAsync(string productId);
    }
}
 