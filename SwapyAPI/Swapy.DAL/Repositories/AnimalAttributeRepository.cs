using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalAttributeRepository : IAnimalAttributeRepository
    {
        private readonly SwapyDbContext context;
    
        public AnimalAttributeRepository(SwapyDbContext context) => this.context = context;
        
        public async Task CreateAsync(AnimalAttribute item)
        {
            await context.AnimalAttributes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalAttribute item)
        {
            context.AnimalAttributes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AnimalAttribute item)
        {
            context.AnimalAttributes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<AnimalAttribute> GetByIdAsync(Guid id)
        {
            var item = await context.AnimalAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AnimalAttribute>> GetAllAsync()
        {
            return await context.AnimalAttributes.ToListAsync();
        }
    }
}
