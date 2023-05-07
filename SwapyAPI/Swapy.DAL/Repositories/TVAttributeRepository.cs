using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVAttributeRepository : ITVAttributeRepository
    {
        private readonly SwapyDbContext _context;
    
        public TVAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(TVAttribute item)
        {
            await _context.TVAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVAttribute item)
        {
            _context.TVAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVAttribute item)
        {
            _context.TVAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<TVAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.TVAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<TVAttribute>> GetAllAsync()
        {
            return await _context.TVAttributes.ToListAsync();
        }

        public async Task<IQueryable<TVAttribute>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.TVAttributes.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.TVAttributes.Skip(pageSize * (page - 1))
                                        .Take(pageSize)
                                        .Include(tv => tv.Product)
                                            .ThenInclude(p => p.Images)
                                        .Include(tv => tv.Product)
                                            .ThenInclude(p => p.City)
                                        .Include(tv => tv.Product)
                                            .ThenInclude(p => p.Currency)
                                        .Include(tv => tv.Product)
                                            .ThenInclude(p => p.Subcategory)
                                        .Include(tv => tv.TVBrand)
                                        .Include(tv => tv.ScreenResolution)
                                        .Include(tv => tv.ScreenDiagonal)
                                        .Include(tv => tv.TVType)
                                        .AsQueryable();
        }

        public async Task<TVAttribute> GetDetailByIdAsync(Guid id)
        {
            var item = await _context.TVAttributes.Include(tv => tv.Product)
                                                    .ThenInclude(p => p.Images)
                                                  .Include(tv => tv.Product)
                                                    .ThenInclude(p => p.City)
                                                  .Include(tv => tv.Product)
                                                    .ThenInclude(p => p.Currency)
                                                  .Include(tv => tv.Product)
                                                    .ThenInclude(p => p.Subcategory)
                                                  .Include(tv => tv.TVBrand)
                                                  .Include(tv => tv.ScreenResolution)
                                                  .Include(tv => tv.ScreenDiagonal)
                                                  .Include(tv => tv.TVType)
                                                  .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
    }
} 