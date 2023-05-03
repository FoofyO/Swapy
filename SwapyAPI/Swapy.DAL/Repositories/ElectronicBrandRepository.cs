using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicBrandRepository : IElectronicBrandRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicBrandRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ElectronicBrand item)
        {
            await context.ElectronicBrands.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ElectronicBrand item)
        {
            context.ElectronicBrands.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ElectronicBrand item)
        {
            context.ElectronicBrands.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ElectronicBrand> GetByIdAsync(Guid id)
        {
            var item = await context.ElectronicBrands.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ElectronicBrand>> GetAllAsync()
        {
            return await context.ElectronicBrands.ToListAsync();
        }
    }
}
