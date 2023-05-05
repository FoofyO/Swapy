using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SwapyDbContext _context;

        public ProductRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Product item)
        {
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            _context.Products.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product item)
        {
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByUserId(Guid userId)
        {
            return await _context.Products.Where(p => p.UserId == userId).ToListAsync();
        }
    }
}
