using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class RealEstateAttributeRepository : IRealEstateAttributeRepository
    {
        private readonly SwapyDbContext _context;
    
        public RealEstateAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(RealEstateAttribute item)
        {
            await _context.RealEstateAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RealEstateAttribute item)
        {
            _context.RealEstateAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RealEstateAttribute item)
        {
            _context.RealEstateAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<RealEstateAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.RealEstateAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<RealEstateAttribute>> GetAllAsync()
        {
            return await _context.RealEstateAttributes.ToListAsync();
        }
    }
}
