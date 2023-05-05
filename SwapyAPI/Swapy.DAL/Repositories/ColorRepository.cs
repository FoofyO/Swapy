using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly SwapyDbContext _context;

        public ColorRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Color item)
        {
            await _context.Colors.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Color item)
        {
            _context.Colors.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Color item)
        {
            _context.Colors.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Color> GetByIdAsync(Guid id)
        {
            var item = await _context.Colors.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Color>> GetAllAsync()
        {
            return await _context.Colors.ToListAsync();
        }
    }
}

