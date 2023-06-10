using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesViewRepository : IRepository<ClothesView>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetByGenderAndTypeAsync(string genderId, string clothesTypeId, Languages language);
    }
}
