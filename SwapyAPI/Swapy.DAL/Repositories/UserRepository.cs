using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SwapyDbContext context;

        public UserRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(User item)
        {
            await context.Users.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User item)
        {
            context.Users.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User item)
        {
            context.Users.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<User> GetByIdAsync(Guid id)
        {
            var item = await context.Users.FindAsync(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await context.Users.ToListAsync();
        }
    }
}
