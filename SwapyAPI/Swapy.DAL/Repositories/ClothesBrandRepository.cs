using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesBrandRepository : IClothesBrandRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesBrandRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesBrand item)
        {
            await _context.ClothesBrands.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesBrand item)
        {
            _context.ClothesBrands.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesBrand item)
        {
            _context.ClothesBrands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ClothesBrand> GetByIdAsync(Guid id)
        {
            var item = await _context.ClothesBrands.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesBrand>> GetAllAsync()
        {
            return await _context.ClothesBrands.ToListAsync();
        }
    }
}
