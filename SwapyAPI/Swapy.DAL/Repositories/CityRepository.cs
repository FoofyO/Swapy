using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly SwapyDbContext context;

        public CityRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(City item)
        {
            await context.Cities.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(City item)
        {
            context.Cities.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(City item)
        {
            context.Cities.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<City> GetByIdAsync(Guid id)
        {
            var item = await context.Cities.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await context.Cities.ToListAsync();
        }
    }
}
