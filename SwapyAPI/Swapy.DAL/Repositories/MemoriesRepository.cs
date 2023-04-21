using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class MemoriesRepository : IMemoriesRepository
    {
        private readonly SwapyDbContext context;

        public MemoriesRepository(SwapyDbContext context) => this.context = context;

        public void Create(Memories item)
        {
            context.Memories.Add(item);
            context.SaveChanges();
        }
        public void Update(Memories item)
        {
            context.Memories.Update(item);
            context.SaveChanges();
        }
        public void Delete(Memories item)
        {
            context.Memories.Remove(item);
            context.SaveChanges();
        }

        public Memories GetById(Guid id)
        {
            var item = context.Memories.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<Memories> GetAll()
        {
            return context.Memories.ToList();
        }
    }
}
