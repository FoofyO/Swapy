namespace Swapy.DAL.Interfaces
{
    public interface IQueryableProviderRepository<T> : IRepository<T>
    {
        Task<IQueryable<T>> GetQueryableAsync();
    }
}
