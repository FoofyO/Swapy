using Microsoft.EntityFrameworkCore;
using Swapy.DAL.Interfaces;
using Swapy.Common.Entities;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandTypeRepository : IAutoBrandTypeRepository
    {
        private readonly SwapyDbContext _context;

        public AutoBrandTypeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(AutoBrandType item)
        {
            await _context.AutoBrandsTypes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoBrandType item)
        {
            _context.AutoBrandsTypes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoBrandType item)
        {
            _context.AutoBrandsTypes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AutoBrandType> GetByIdAsync(Guid id)
        {
            var item = await _context.AutoBrandsTypes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AutoBrandType>> GetAllAsync()
        {
            return await _context.AutoBrandsTypes.ToListAsync();
        }
    }
}
