using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandRepository : IAutoBrandRepository
    {
        private readonly SwapyDbContext _context;

        public AutoBrandRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(AutoBrand item)
        {
            await _context.AutoBrands.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoBrand item)
        {
            _context.AutoBrands.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoBrand item)
        {
            _context.AutoBrands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<AutoBrand> GetByIdAsync(Guid id)
        {
            var item = await _context.AutoBrands.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AutoBrand>> GetAllAsync()
        {
            return await _context.AutoBrands.ToListAsync();
        }
    }
}

