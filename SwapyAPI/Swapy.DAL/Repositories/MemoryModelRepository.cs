using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MemoryModelRepository : IMemoryModelRepository
    {
        private readonly SwapyDbContext context;

        public MemoryModelRepository(SwapyDbContext context) => this.context = context;

        public void Create(MemoryModel item)
        {
            context.MemoriesModels.Add(item);
            context.SaveChanges();
        }

        public void Update(MemoryModel item)
        {
            context.MemoriesModels.Update(item);
            context.SaveChanges();
        }

        public void Delete(MemoryModel item)
        {
            context.MemoriesModels.Remove(item);
            context.SaveChanges();
        }

        public MemoryModel GetById(Guid id)
        {
            var item = context.MemoriesModels.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<MemoryModel> GetAll()
        {
            return context.MemoriesModels.ToList();
        }
    }
}
