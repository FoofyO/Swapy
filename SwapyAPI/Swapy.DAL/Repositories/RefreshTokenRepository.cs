using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class RefreshTokenRepository : IRefreshToken
    {
        private readonly SwapyDbContext _context;

        public RefreshTokenRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(RefreshToken item)
        {
            await _context.RefreshTokens.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RefreshToken item)
        {
            _context.RefreshTokens.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RefreshToken item)
        {
            _context.RefreshTokens.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));
        
        public async Task<RefreshToken> GetByIdAsync(Guid id)
        {
            var item = await _context.RefreshTokens.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<RefreshToken>> GetAllAsync()
        {
            return await _context.RefreshTokens.ToListAsync();
        }
    }
}
