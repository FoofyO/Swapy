using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubscribeRepository : ISubscriptionRepository
    {
        private readonly SwapyDbContext _context;

        public SubscribeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Subscription item)
        {
            await _context.Subscriptions.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subscription item)
        {
            _context.Subscriptions.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subscription item)
        {
            _context.Subscriptions.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Subscription> GetByIdAsync(string id)
        {
            var item = await _context.Subscriptions.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Subscription>> GetAllAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }
    }
}
