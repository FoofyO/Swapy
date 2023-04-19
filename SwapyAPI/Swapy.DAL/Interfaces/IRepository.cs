using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Swapy.DAL.Interfaces
{
    public interface IRepository<T>
    {
        T GetById(Guid id);
        void Create(T item);
        void Update(T item);
        void Delete(T item);
        IEnumerable<T> GetAll();
    }
}
