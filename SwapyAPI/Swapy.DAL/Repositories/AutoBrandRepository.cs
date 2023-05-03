using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandRepository : IAutoBrandRepository
    {
        private readonly SwapyDbContext context;

        public AutoBrandRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(AutoBrand item)
        {
            await context.AutoBrands.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoBrand item)
        {
            context.AutoBrands.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoBrand item)
        {
            context.AutoBrands.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<AutoBrand> GetByIdAsync(Guid id)
        {
            var item = await context.AutoBrands.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AutoBrand>> GetAllAsync()
        {
            return await context.AutoBrands.ToListAsync();
        }
    }
}

