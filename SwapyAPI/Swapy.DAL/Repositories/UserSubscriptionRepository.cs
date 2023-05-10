using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class UserSubscriptionRepository : IUserSubscriptionRepository
    {
        private readonly SwapyDbContext _context;

        public UserSubscriptionRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(UserSubscription item)
        {
            await _context.UsersSubscriptions.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserSubscription item)
        {
            _context.UsersSubscriptions.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserSubscription item)
        {
            _context.UsersSubscriptions.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<UserSubscription> GetByIdAsync(string id)
        {
            var item = await _context.UsersSubscriptions.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<UserSubscription>> GetAllAsync()
        {
            return await _context.UsersSubscriptions.ToListAsync();
        }
    }
}
