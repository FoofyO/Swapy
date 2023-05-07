using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SwapyDbContext _context;

        public UserRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(User item)
        {
            await _context.Users.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(User item)
        {
            _context.Users.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User item)
        {
            _context.Users.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<User> GetByIdAsync(Guid id)
        {
            var item = await _context.Users.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
        
        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
