using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubscribeRepository : ISubscribeRepository
    {
        private readonly SwapyDbContext context;

        public SubscribeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Subscribe item)
        {
            await context.Subscribes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subscribe item)
        {
            context.Subscribes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subscribe item)
        {
            context.Subscribes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Subscribe> GetByIdAsync(Guid id)
        {
            var item = await context.Subscribes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Subscribe>> GetAllAsync()
        {
            return await context.Subscribes.ToListAsync();
        }
    }
}
