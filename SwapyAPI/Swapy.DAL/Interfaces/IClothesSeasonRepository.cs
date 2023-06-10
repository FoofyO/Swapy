using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;

namespace Swapy.DAL.Interfaces
{
    public interface IClothesSeasonRepository : IRepository<ClothesSeason>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAsync(Languages language);
    }
}
