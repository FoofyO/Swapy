using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesViewRepository : IClothesViewRepository
    {
        private readonly SwapyDbContext context;

        public ClothesViewRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ClothesView item)
        {
            await context.ClothesViews.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesView item)
        {
            context.ClothesViews.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesView item)
        {
            context.ClothesViews.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ClothesView> GetByIdAsync(Guid id)
        {
            var item = await context.ClothesViews.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesView>> GetAllAsync()
        {
            return await context.ClothesViews.ToListAsync();
        }
    }
}
