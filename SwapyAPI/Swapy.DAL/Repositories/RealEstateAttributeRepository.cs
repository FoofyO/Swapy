using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class RealEstateAttributeRepository : IRealEstateAttributeRepository
    {
        private readonly SwapyDbContext context;
    
        public RealEstateAttributeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(RealEstateAttribute item)
        {
            await context.RealEstateAttributes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RealEstateAttribute item)
        {
            context.RealEstateAttributes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RealEstateAttribute item)
        {
            context.RealEstateAttributes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<RealEstateAttribute> GetByIdAsync(Guid id)
        {
            var item = await context.RealEstateAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<RealEstateAttribute>> GetAllAsync()
        {
            return await context.RealEstateAttributes.ToListAsync();
        }
    }
}
