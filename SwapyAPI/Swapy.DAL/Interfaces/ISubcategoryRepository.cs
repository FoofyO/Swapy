using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory> GetDetailByIdAsync(string id);
        Task<IEnumerable<Subcategory>> GetByCategoryAsync(string categoryId);
        Task<IEnumerable<Subcategory>> GetBySubcategoryAsync(string subcategoryId);
    }
}
