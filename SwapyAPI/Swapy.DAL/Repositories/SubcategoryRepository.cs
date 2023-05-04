using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly SwapyDbContext _context;

        public SubcategoryRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Subcategory item)
        {
            await _context.Subcategories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subcategory item)
        {
            _context.Subcategories.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subcategory item)
        {
            _context.Subcategories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<Subcategory> GetByIdAsync(Guid id)
        {
            var item = await _context.Subcategories.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Subcategory>> GetAllAsync()
        {
            return await _context.Subcategories.ToListAsync();
        }
    }
}
