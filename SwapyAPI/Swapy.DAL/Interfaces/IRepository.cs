namespace Swapy.DAL.Interfaces
{
    public interface IRepository<T>
    {
        Task<T> GetByIdAsync(Guid id);
        Task CreateAsync(T item);
        Task UpdateAsync(T item);
        Task DeleteAsync(T item);
        Task DeleteByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
