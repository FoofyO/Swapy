using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly SwapyDbContext _context;

        public LikeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Like item)
        {
            await _context.Likes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Like item)
        {
            _context.Likes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Like item)
        {
            _context.Likes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Like> GetByIdAsync(Guid id)
        {
            var item = await _context.Likes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Like>> GetAllAsync()
        {
            return await _context.Likes.ToListAsync();
        }
    }
}
