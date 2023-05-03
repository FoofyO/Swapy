using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVBrandRepository : ITVBrandRepository
    {
        private readonly SwapyDbContext context;

        public TVBrandRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(TVBrand item)
        {
            await context.TVBrands.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVBrand item)
        {
            context.TVBrands.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVBrand item)
        {
            context.TVBrands.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<TVBrand> GetByIdAsync(Guid id)
        {
            var item = await context.TVBrands.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<TVBrand>> GetAllAsync()
        {
            return await context.TVBrands.ToListAsync();
        }
    }
}
