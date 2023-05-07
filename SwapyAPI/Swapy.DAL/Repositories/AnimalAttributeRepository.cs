using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
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
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<AnimalAttribute>> GetAllAsync()
        {
            return await _context.AnimalAttributes.ToListAsync();
        }

        public async Task<IQueryable<AnimalAttribute>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.AnimalAttributes.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.AnimalAttributes.Skip(pageSize * (page - 1))
                                            .Take(pageSize)
                                            .Include(a => a.Product)
                                                .ThenInclude(p => p.Images)
                                            .Include(a => a.Product)
                                                .ThenInclude(p => p.City)
                                            .Include(a => a.Product)
                                                .ThenInclude(p => p.Currency)
                                            .Include(a => a.Product)
                                                .ThenInclude(p => p.Subcategory)
                                            .Include(a => a.AnimalBreed)
                                                .ThenInclude(ab => ab.AnimalType)
                                            .AsQueryable();
        }

        public async Task<AnimalAttribute> GetDetailByIdAsync(Guid id)
        {
            var item = await _context.AnimalAttributes.Include(a => a.Product)
                                                        .ThenInclude(p => p.Images)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.City)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.Currency)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.Subcategory)
                                                      .Include(a => a.AnimalBreed)
                                                        .ThenInclude(ab => ab.AnimalType)
                                                      .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
    }
}
