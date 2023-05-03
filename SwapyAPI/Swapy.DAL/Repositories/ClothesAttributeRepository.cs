using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesAttributeRepository : IClothesAttributeRepository
    {
        private readonly SwapyDbContext context;

        public ClothesAttributeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ClothesAttribute item)
        {
            await context.ClothesAttributes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesAttribute item)
        {
            context.ClothesAttributes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesAttribute item)
        {
            context.ClothesAttributes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ClothesAttribute> GetByIdAsync(Guid id)
        {
            var item = await context.ClothesAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesAttribute>> GetAllAsync()
        {
            return await context.ClothesAttributes.ToListAsync();
        }
    }
}
 