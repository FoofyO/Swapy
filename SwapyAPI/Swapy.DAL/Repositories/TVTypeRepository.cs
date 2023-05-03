using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVTypeRepository : ITVTypeRepository
    {
        private readonly SwapyDbContext context;

        public TVTypeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(TVType item)
        {
            await context.TVTypes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVType item)
        {
            context.TVTypes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVType item)
        {
            context.TVTypes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<TVType> GetByIdAsync(Guid id)
        {
            var item = await context.TVTypes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<TVType>> GetAllAsync()
        {
            return await context.TVTypes.ToListAsync();
        }
    }
}
