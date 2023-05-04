using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubscribeRepository : ISubscribeRepository
    {
        private readonly SwapyDbContext _context;

        public SubscribeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Subscribe item)
        {
            await _context.Subscribes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subscribe item)
        {
            _context.Subscribes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subscribe item)
        {
            _context.Subscribes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Subscribe> GetByIdAsync(Guid id)
        {
            var item = await _context.Subscribes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Subscribe>> GetAllAsync()
        {
            return await _context.Subscribes.ToListAsync();
        }
    }
}
