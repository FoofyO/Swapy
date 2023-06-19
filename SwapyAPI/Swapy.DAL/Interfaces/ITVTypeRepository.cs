using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;

namespace Swapy.DAL.Interfaces
{
    public interface ITVTypeRepository : IRepository<TVType>
    {
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAsync(Language language);
    }
}
