using Swapy.Common.Entities;
using Swapy.Common.Enums;

namespace Swapy.DAL.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory> GetDetailByIdAsync(string id);
        Task<IEnumerable<Subcategory>> GetByCategoryAsync(string categoryId);
        Task<IEnumerable<Subcategory>> GetBySubcategoryAsync(string subcategoryId);
        Task<IEnumerable<Subcategory>> GetAllAutoTypesAsync();
        Task<IEnumerable<Subcategory>> GetClothesTypesByGenderAsync(string genderId);
        Task<IEnumerable<Subcategory>> GetAllElectronicTypesAsync();
        Task<IEnumerable<Subcategory>> GetAllRealEstateTypesAsync();
        Task<IEnumerable<Subcategory>> GetAllAnimalTypesAsync();
        Task<IEnumerable<Subcategory>> GetAllItemSectionsAsync();
        Task<IEnumerable<Subcategory>> GetAllItemTypesAsync(string parentSubcategoryId);
        Task<IEnumerable<Subcategory>> GetSequenceOfSubcategories(string subcategoryId);
    }
}
