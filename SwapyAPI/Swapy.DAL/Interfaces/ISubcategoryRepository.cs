using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface ISubcategoryRepository : IRepository<Subcategory>
    {
        Task<Subcategory> GetDetailByIdAsync(Guid id);
    }
}
