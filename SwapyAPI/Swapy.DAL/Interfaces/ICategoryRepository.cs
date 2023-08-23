using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;

namespace Swapy.DAL.Interfaces
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task<IEnumerable<CategoryTreeResponseDTO>> GetAllAsync(Language language);
        Task<SpecificationResponseDTO<string>> GetBySubcategoryIdAsync(string subcategoryId, Language language);
    }
}
