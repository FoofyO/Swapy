using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Interfaces
{
    public interface IAttributeRepositoryy<T> : IRepository<T>
    {
        Task<IQueryable<T>> GetByPageAsync(int page, int pageSize);
        Task<T> GetDetailByIdAsync(Guid id);
    }
}
