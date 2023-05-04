using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicBrandRepository : IElectronicBrandRepository
    {
        private readonly SwapyDbContext _context;

        public ElectronicBrandRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ElectronicBrand item)
        {
            await _context.ElectronicBrands.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ElectronicBrand item)
        {
            _context.ElectronicBrands.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ElectronicBrand item)
        {
            _context.ElectronicBrands.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ElectronicBrand> GetByIdAsync(Guid id)
        {
            var item = await _context.ElectronicBrands.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ElectronicBrand>> GetAllAsync()
        {
            return await _context.ElectronicBrands.ToListAsync();
        }
    }
}
