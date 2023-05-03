using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SwapyDbContext context;

        public ProductRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Product item)
        {
            await context.Products.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            context.Products.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product item)
        {
            context.Products.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var item = await context.Products.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByUserId(Guid userId)
        {
            return await context.Products.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
