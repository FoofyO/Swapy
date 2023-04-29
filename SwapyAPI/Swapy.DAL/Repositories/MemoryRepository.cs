using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MemoryRepository : IMemoryRepository
    {
        private readonly SwapyDbContext context;

        public MemoryRepository(SwapyDbContext context) => this.context = context;

        public void Create(Memory item)
        {
            context.Memories.Add(item);
            context.SaveChanges();
        }

        public void Update(Memory item)
        {
            context.Memories.Update(item);
            context.SaveChanges();
        }

        public void Delete(Memory item)
        {
            context.Memories.Remove(item);
            context.SaveChanges();
        }

        public Memory GetById(Guid id)
        {
            var item = context.Memories.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<Memory> GetAll()
        {
            return context.Memories.ToList();
        }
    }
}
