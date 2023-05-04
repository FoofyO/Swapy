using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesSeasonRepository : IClothesSeasonRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesSeasonRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesSeason item)
        {
            await _context.ClothesSeasons.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesSeason item)
        {
            _context.ClothesSeasons.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesSeason item)
        {
            _context.ClothesSeasons.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ClothesSeason> GetByIdAsync(Guid id)
        {
            var item = await _context.ClothesSeasons.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesSeason>> GetAllAsync()
        {
            return await _context.ClothesSeasons.ToListAsync();
        }
    }
}
