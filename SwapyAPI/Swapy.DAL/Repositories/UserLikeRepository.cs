using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class UserLikeRepository : IUserLikeRepository
    {
        private readonly SwapyDbContext _context;

        public UserLikeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(UserLike item)
        {
            await _context.UsersLikes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(UserLike item)
        {
            _context.UsersLikes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(UserLike item)
        {
            _context.UsersLikes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<UserLike> GetByIdAsync(string id)
        {
            var item = await _context.UsersLikes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<UserLike>> GetAllAsync()
        {
            return await _context.UsersLikes.ToListAsync();
        }
    }
}
