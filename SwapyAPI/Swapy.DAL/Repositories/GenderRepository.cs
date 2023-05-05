using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class GenderRepository : IGenderRepository
    {
        private readonly SwapyDbContext _context;

        public GenderRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Gender item)
        {
            await _context.Genders.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Gender item)
        {
            _context.Genders.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Gender item)
        {
            _context.Genders.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Gender> GetByIdAsync(Guid id)
        {
            var item = await _context.Genders.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Gender>> GetAllAsync()
        {
            return await _context.Genders.ToListAsync();
        }
    }
}
