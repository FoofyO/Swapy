using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesBrandViewRepository : IClothesBrandViewRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesBrandViewRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesBrandView item)
        {
            await _context.ClothesBrandsViews.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesBrandView item)
        {
            _context.ClothesBrandsViews.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesBrandView item)
        {
            _context.ClothesBrandsViews.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ClothesBrandView> GetByIdAsync(Guid id)
        {
            var item = await _context.ClothesBrandsViews.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesBrandView>> GetAllAsync()
        {
            return await _context.ClothesBrandsViews.ToListAsync();
        }
    }
}
