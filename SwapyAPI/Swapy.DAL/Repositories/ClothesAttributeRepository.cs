using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesAttributeRepository : IClothesAttributeRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesAttribute item)
        {
            await _context.ClothesAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesAttribute item)
        {
            _context.ClothesAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesAttribute item)
        {
            _context.ClothesAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ClothesAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.ClothesAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesAttribute>> GetAllAsync()
        {
            return await _context.ClothesAttributes.ToListAsync();
        }
    }
}
 