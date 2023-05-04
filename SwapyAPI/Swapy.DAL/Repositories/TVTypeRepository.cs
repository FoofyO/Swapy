using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVTypeRepository : ITVTypeRepository
    {
        private readonly SwapyDbContext _context;

        public TVTypeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(TVType item)
        {
            await _context.TVTypes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVType item)
        {
            _context.TVTypes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVType item)
        {
            _context.TVTypes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<TVType> GetByIdAsync(Guid id)
        {
            var item = await _context.TVTypes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<TVType>> GetAllAsync()
        {
            return await _context.TVTypes.ToListAsync();
        }
    }
}
