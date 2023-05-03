using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesBrandViewRepository : IClothesBrandViewRepository
    {
        private readonly SwapyDbContext context;

        public ClothesBrandViewRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ClothesBrandView item)
        {
            await context.ClothesBrandsViews.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesBrandView item)
        {
            context.ClothesBrandsViews.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesBrandView item)
        {
            context.ClothesBrandsViews.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ClothesBrandView> GetByIdAsync(Guid id)
        {
            var item = await context.ClothesBrandsViews.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesBrandView>> GetAllAsync()
        {
            return await context.ClothesBrandsViews.ToListAsync();
        }
    }
}
