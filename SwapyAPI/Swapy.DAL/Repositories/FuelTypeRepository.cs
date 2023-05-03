using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class FuelTypeRepository : IFuelTypeRepository
    {
        private readonly SwapyDbContext context;

        public FuelTypeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(FuelType item)
        {
            await context.FuelTypes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FuelType item)
        {
            context.FuelTypes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FuelType item)
        {
            context.FuelTypes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<FuelType> GetByIdAsync(Guid id)
        {
            var item = await context.FuelTypes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<FuelType>> GetAllAsync()
        {
            return await context.FuelTypes.ToListAsync();
        }
    }
}
