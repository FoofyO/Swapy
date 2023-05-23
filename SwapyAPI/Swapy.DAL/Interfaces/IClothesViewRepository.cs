using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesViewRepository : IRepository<ClothesView>
    {
        Task<IEnumerable<ClothesView>> GetByGenderAndTypeAsync(string genderId, string clothesTypeId);
    }
}
