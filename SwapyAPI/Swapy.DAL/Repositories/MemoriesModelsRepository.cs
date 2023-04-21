using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class MemoriesModelsRepository : IMemoriesModelsRepository
    {
        private readonly SwapyDbContext context;

        public MemoriesModelsRepository(SwapyDbContext context) => this.context = context;

        public void Create(MemoriesModels item)
        {
            context.MemoriesModels.Add(item);
            context.SaveChanges();
        }
        public void Update(MemoriesModels item)
        {
            context.MemoriesModels.Update(item);
            context.SaveChanges();
        }
        public void Delete(MemoriesModels item)
        {
            context.MemoriesModels.Remove(item);
            context.SaveChanges();
        }

        public MemoriesModels GetById(Guid id)
        {
            var item = context.MemoriesModels.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<MemoriesModels> GetAll()
        {
            return context.MemoriesModels.ToList();
        }
    }
}
