using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MemoryModelRepository : IMemoryModelRepository
    {
        private readonly SwapyDbContext context;

        public MemoryModelRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(MemoryModel item)
        {
            await context.MemoriesModels.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(MemoryModel item)
        {
            context.MemoriesModels.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(MemoryModel item)
        {
            context.MemoriesModels.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<MemoryModel> GetByIdAsync(Guid id)
        {
            var item = await context.MemoriesModels.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<MemoryModel>> GetAllAsync()
        {
            return await context.MemoriesModels.ToListAsync();
        }
    }
}
