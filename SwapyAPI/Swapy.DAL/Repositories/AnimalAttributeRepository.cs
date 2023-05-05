using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalAttributeRepository : IAnimalAttributeRepository
    {
        private readonly SwapyDbContext _context;
    
        public AnimalAttributeRepository(SwapyDbContext context) => _context = context;
        
        public async Task CreateAsync(AnimalAttribute item)
        {
            await _context.AnimalAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalAttribute item)
        {
            _context.AnimalAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AnimalAttribute item)
        {
            _context.AnimalAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AnimalAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.AnimalAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AnimalAttribute>> GetAllAsync()
        {
            return await _context.AnimalAttributes.ToListAsync();
        }
    }
}
