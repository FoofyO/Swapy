using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ModelColorRepository : IModelColorRepository
    {
        private readonly SwapyDbContext _context;

        public ModelColorRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ModelColor item)
        {
            await _context.ModelsColors.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ModelColor item)
        {
            _context.ModelsColors.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ModelColor item)
        {
            _context.ModelsColors.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task<ModelColor> GetByIdAsync(Guid id)
        {
            var item = await _context.ModelsColors.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ModelColor>> GetAllAsync()
        {
            return await _context.ModelsColors.ToListAsync();
        }
    }
}
