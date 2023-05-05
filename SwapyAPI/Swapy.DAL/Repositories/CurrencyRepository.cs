using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly SwapyDbContext _context;

        public CurrencyRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Currency item)
        {
            await _context.Currencies.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Currency item)
        {
            _context.Currencies.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Currency item)
        {
            _context.Currencies.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Currency> GetByIdAsync(Guid id)
        {
            var item = await _context.Currencies.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await _context.Currencies.ToListAsync();
        }
    }
}
