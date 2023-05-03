using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoAttributeRepository : IAutoAttributeRepository
    {
        private readonly SwapyDbContext context;
    
        public AutoAttributeRepository(SwapyDbContext context) => this.context = context;
        
        public async Task CreateAsync(AutoAttribute item)
        {
            await context.AutoAttributes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoAttribute item)
        {
            context.AutoAttributes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoAttribute item)
        {
            context.AutoAttributes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<AutoAttribute> GetByIdAsync(Guid id)
        {
            var item = await context.AutoAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AutoAttribute>> GetAllAsync()
        {
            return await context.AutoAttributes.ToListAsync();
        }
    }
}
