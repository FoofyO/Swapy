using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{ 
    public class ScreenResolutionRepository : IScreenResolutionRepository
    {
        private readonly SwapyDbContext context;

        public ScreenResolutionRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ScreenResolution item)
        {
            await context.ScreenResolutions.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ScreenResolution item)
        {
            context.ScreenResolutions.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ScreenResolution item)
        {
            context.ScreenResolutions.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ScreenResolution> GetByIdAsync(Guid id)
        {
            var item = await context.ScreenResolutions.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ScreenResolution>> GetAllAsync()
        {
            return await context.ScreenResolutions.ToListAsync();
        }
    }
}
