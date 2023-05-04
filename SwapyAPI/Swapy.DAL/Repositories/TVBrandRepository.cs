using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVBrandRepository : ITVBrandRepository
    {
        private readonly SwapyDbContext _context;

        public TVBrandRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(TVBrand item)
        {
            await _context.TVBrands.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVBrand item)
        {
            _context.TVBrands.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVBrand item)
        {
            _context.TVBrands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<TVBrand> GetByIdAsync(Guid id)
        {
            var item = await _context.TVBrands.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<TVBrand>> GetAllAsync()
        {
            return await _context.TVBrands.ToListAsync();
        }
    }
}
