using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly SwapyDbContext _context;

        public ModelRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Model item)
        {
            await _context.Models.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Model item)
        {
            _context.Models.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Model item)
        {
            _context.Models.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Model> GetByIdAsync(string id)
        {
            var item = await _context.Models.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await _context.Models.ToListAsync();
        }

        public async Task<IQueryable<Model>> GetQueryableAsync()
        {
            return _context.Models.AsQueryable();
        }
    }
}
