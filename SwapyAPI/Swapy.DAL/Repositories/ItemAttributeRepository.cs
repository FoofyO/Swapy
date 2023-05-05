using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ItemAttributeRepository : IItemAttributeRepository
    {
        private readonly SwapyDbContext _context;

        public ItemAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ItemAttribute item)
        {
            await _context.ItemAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ItemAttribute item)
        {
            _context.ItemAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ItemAttribute item)
        {
            _context.ItemAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ItemAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.ItemAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ItemAttribute>> GetAllAsync()
        {
            return await _context.ItemAttributes.ToListAsync();
        }
    }
}
