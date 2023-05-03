using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ScreenDiagonalRepository : IScreenDiagonalRepository
    {
        private readonly SwapyDbContext context;

        public ScreenDiagonalRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ScreenDiagonal item)
        {
            await context.ScreenDiagonals.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ScreenDiagonal item)
        {
            context.ScreenDiagonals.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ScreenDiagonal item)
        {
            context.ScreenDiagonals.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ScreenDiagonal> GetByIdAsync(Guid id)
        {
            var item = await context.ScreenDiagonals.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ScreenDiagonal>> GetAllAsync()
        {
            return await context.ScreenDiagonals.ToListAsync();
        }
    }
}
