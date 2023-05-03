using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MemoryRepository : IMemoryRepository
    {
        private readonly SwapyDbContext context;

        public MemoryRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Memory item)
        {
            await context.Memories.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Memory item)
        {
            context.Memories.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Memory item)
        {
            context.Memories.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Memory> GetByIdAsync(Guid id)
        {
            var item = await context.Memories.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Memory>> GetAllAsync()
        {
            return await context.Memories.ToListAsync();
        }
    }
}
