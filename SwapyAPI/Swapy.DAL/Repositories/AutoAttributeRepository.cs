using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoAttributeRepository : IAutoAttributeRepository
    {
        private readonly SwapyDbContext _context;
    
        public AutoAttributeRepository(SwapyDbContext context) => _context = context;
        
        public async Task CreateAsync(AutoAttribute item)
        {
            await _context.AutoAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoAttribute item)
        {
            _context.AutoAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoAttribute item)
        {
            _context.AutoAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AutoAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.AutoAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AutoAttribute>> GetAllAsync()
        {
            return await _context.AutoAttributes.ToListAsync();
        }
    }
}
