using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly SwapyDbContext context;

        public GenderRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Gender item)
        {
            await context.Genders.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Gender item)
        {
            context.Genders.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Gender item)
        {
            context.Genders.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Gender> GetByIdAsync(Guid id)
        {
            var item = await context.Genders.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            return await context.Genders.ToListAsync();
        }
    }
}
