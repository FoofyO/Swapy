using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVAttributeRepository : ITVAttributeRepository
    {
        private readonly SwapyDbContext context;
    
        public TVAttributeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(TVAttribute item)
        {
            await context.TVAttributes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVAttribute item)
        {
            context.TVAttributes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVAttribute item)
        {
            context.TVAttributes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<TVAttribute> GetByIdAsync(Guid id)
        {
            var item = await context.TVAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<TVAttribute>> GetAllAsync()
        {
            return await context.TVAttributes.ToListAsync();
        }
    }
} 
