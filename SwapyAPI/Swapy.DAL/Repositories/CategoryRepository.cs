using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SwapyDbContext context;

        public CategoryRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Category item)
        {
            await context.Categories.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category item)
        {
            context.Categories.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category item)
        {
            context.Categories.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var item = await context.Categories.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await context.Categories.ToListAsync();
        }
    }
}
