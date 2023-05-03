using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicBrandTypeRepository : IElectronicBrandTypeRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicBrandTypeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ElectronicBrandType item)
        {
            await context.ElectronicBrandsTypes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ElectronicBrandType item)
        {
            context.ElectronicBrandsTypes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ElectronicBrandType item)
        {
            context.ElectronicBrandsTypes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ElectronicBrandType> GetByIdAsync(Guid id)
        {
            var item = await context.ElectronicBrandsTypes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ElectronicBrandType>> GetAllAsync()
        {
            return await context.ElectronicBrandsTypes.ToListAsync();
        }
    }
}
