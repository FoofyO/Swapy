using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly SwapyDbContext context;

        public SubcategoryRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Subcategory item)
        {
            await context.Subcategories.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subcategory item)
        {
            context.Subcategories.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subcategory item)
        {
            context.Subcategories.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Subcategory> GetByIdAsync(Guid id)
        {
            var item = await context.Subcategories.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Subcategory>> GetAllAsync()
        {
            return await context.Subcategories.ToListAsync();
        }
    }
}
