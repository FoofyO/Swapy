using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesAttributeRepository : IAttributeRepository<ClothesAttribute>
    {
        Task<ClothesAttribute> GetByProductIdAsync(string productId);
    }
}
