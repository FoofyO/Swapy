using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IAttributeRepository<T> : IRepository<T>
    {
        Task<IQueryable<T>> GetByPageAsync(int page, int pageSize);
        Task<T> GetDetailByIdAsync(string id);
        
    }
}
