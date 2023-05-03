using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly SwapyDbContext context;

        public ProductImageRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ProductImage item)
        {
            await context.ProductImages.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductImage item)
        {
            context.ProductImages.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductImage item)
        {
            context.ProductImages.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ProductImage> GetByIdAsync(Guid id)
        {
            var item = await context.ProductImages.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            return await context.ProductImages.ToListAsync();
        }
    }
}
