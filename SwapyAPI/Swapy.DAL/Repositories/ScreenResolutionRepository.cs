using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{ 
    public class ScreenResolutionRepository : IScreenResolutionRepository
    {
        private readonly SwapyDbContext _context;

        public ScreenResolutionRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ScreenResolution item)
        {
            await _context.ScreenResolutions.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ScreenResolution item)
        {
            _context.ScreenResolutions.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ScreenResolution item)
        {
            _context.ScreenResolutions.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ScreenResolution> GetByIdAsync(Guid id)
        {
            var item = await _context.ScreenResolutions.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ScreenResolution>> GetAllAsync()
        {
            return await _context.ScreenResolutions.ToListAsync();
        }
    }
}
