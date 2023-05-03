using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly SwapyDbContext context;

        public CurrencyRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Currency item)
        {
            await context.Currencies.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Currency item)
        {
            context.Currencies.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Currency item)
        {
            context.Currencies.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Currency> GetByIdAsync(Guid id)
        {
            var item = await context.Currencies.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Currency>> GetAllAsync()
        {
            return await context.Currencies.ToListAsync();
        }
    }
}
