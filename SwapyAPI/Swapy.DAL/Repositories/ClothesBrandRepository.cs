using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesBrandRepository : IClothesBrandRepository
    {
        private readonly SwapyDbContext context;

        public ClothesBrandRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ClothesBrand item)
        {
            await context.ClothesBrands.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesBrand item)
        {
            context.ClothesBrands.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesBrand item)
        {
            context.ClothesBrands.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ClothesBrand> GetByIdAsync(Guid id)
        {
            var item = await context.ClothesBrands.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesBrand>> GetAllAsync()
        {
            return await context.ClothesBrands.ToListAsync();
        }
    }
}
