using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Models;

namespace Swapy.DAL.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory> GetDetailByIdAsync(string id);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetByCategoryAsync(string categoryId, Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetBySubcategoryAsync(string subcategoryId, Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAutoTypesAsync(Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetClothesTypesByGenderAsync(string genderId, Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllElectronicTypesAsync(Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllRealEstateTypesAsync(Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAnimalTypesAsync(Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemSectionsAsync(Language language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemTypesAsync(string parentSubcategoryId, Language language);
        Task<IEnumerable<CategoryNode>> GetSequenceOfSubcategories(string subcategoryId, Language language);
    }
}
