using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
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

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Subcategory> GetByIdAsync(Guid id)
        {
            var item = await _context.Subcategories.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<Subcategory> GetDetailByIdAsync(Guid id)
        {
            var item = await _context.Subcategories.Include(s => s.Subcategories).FirstOrDefaultAsync(s => s.Id == id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Subcategory>> GetAllAsync()
        {
            return await _context.Subcategories.ToListAsync();
        }
    }
}
