using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;

namespace Swapy.DAL.Interfaces
{
    public interface IColorRepository : IRepository<Color>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetByModelAsync(string modelId, Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAsync(Language language);
    }
}
