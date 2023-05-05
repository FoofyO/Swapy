using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVAttributeRepository : ITVAttributeRepository
    {
        private readonly SwapyDbContext _context;
    
        public TVAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(TVAttribute item)
        {
            await _context.TVAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVAttribute item)
        {
            _context.TVAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVAttribute item)
        {
            _context.TVAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<TVAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.TVAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<TVAttribute>> GetAllAsync()
        {
            return await _context.TVAttributes.ToListAsync();
        }
    }
} 
