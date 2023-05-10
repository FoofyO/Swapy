using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ShopAttributeRepository : IShopAttributeRepository
    {
        private readonly SwapyDbContext _context;

        public ShopAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ShopAttribute item)
        {
            await _context.ShopAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ShopAttribute item)
        {
            _context.ShopAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ShopAttribute item)
        {
            _context.ShopAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ShopAttribute> GetByIdAsync(string id)
        {
            var item = await _context.ShopAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ShopAttribute>> GetAllAsync()
        {
            return await _context.ShopAttributes.ToListAsync();
        }
    }
}
