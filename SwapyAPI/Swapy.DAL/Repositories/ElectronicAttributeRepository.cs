using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicAttributeRepository : IElectronicAttributeRepository
    {
        private readonly SwapyDbContext _context;

        public ElectronicAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ElectronicAttribute item)
        {
            await _context.ElectronicAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ElectronicAttribute item)
        {
            _context.ElectronicAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ElectronicAttribute item)
        {
            _context.ElectronicAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ElectronicAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.ElectronicAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ElectronicAttribute>> GetAllAsync()
        {
            return await _context.ElectronicAttributes.ToListAsync();
        }
    }
}
