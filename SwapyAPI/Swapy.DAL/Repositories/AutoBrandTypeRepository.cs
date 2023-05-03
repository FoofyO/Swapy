using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Interfaces;
using Swapy.Common.Entities;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandTypeRepository : IAutoBrandTypeRepository
    {
        private readonly SwapyDbContext context;

        public AutoBrandTypeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(AutoBrandType item)
        {
            await context.AutoBrandsTypes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoBrandType item)
        {
            context.AutoBrandsTypes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoBrandType item)
        {
            context.AutoBrandsTypes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<AutoBrandType> GetByIdAsync(Guid id)
        {
            var item = await context.AutoBrandsTypes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AutoBrandType>> GetAllAsync()
        {
            return await context.AutoBrandsTypes.ToListAsync();
        }
    }
}
