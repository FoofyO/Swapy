using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesSizeRepository : IClothesSizeRepository
    {
        private readonly SwapyDbContext context;

        public ClothesSizeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ClothesSize item)
        {
            await context.ClothesSizes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesSize item)
        {
            context.ClothesSizes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesSize item)
        {
            context.ClothesSizes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ClothesSize> GetByIdAsync(Guid id)
        {
            var item = await context.ClothesSizes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesSize>> GetAllAsync()
        {
            return await context.ClothesSizes.ToListAsync();
        }
    }
}
