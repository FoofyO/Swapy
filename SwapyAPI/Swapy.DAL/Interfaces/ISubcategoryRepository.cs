using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Models;

namespace Swapy.DAL.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory> GetDetailByIdAsync(string id);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetByCategoryAsync(string categoryId, Languages language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetBySubcategoryAsync(string subcategoryId, Languages language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAutoTypesAsync(Languages language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetClothesTypesByGenderAsync(string genderId, Languages language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllElectronicTypesAsync(Languages language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllRealEstateTypesAsync(Languages language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAnimalTypesAsync(Languages language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemSectionsAsync(Languages language);
        Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllItemTypesAsync(string parentSubcategoryId, Languages language);
        Task<IEnumerable<CategoryNode>> GetSequenceOfSubcategories(string subcategoryId, Languages language);
    }
}
