using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly SwapyDbContext context;

        public ColorRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Color item)
        {
            await context.Colors.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Color item)
        {
            context.Colors.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Color item)
        {
            context.Colors.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Color> GetByIdAsync(Guid id)
        {
            var item = await context.Colors.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            return await context.Colors.ToListAsync();
        }
    }
}

