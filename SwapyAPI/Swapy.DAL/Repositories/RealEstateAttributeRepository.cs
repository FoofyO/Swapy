using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class RealEstateAttributeRepository : IRealEstateAttributeRepository
    {
        private readonly SwapyDbContext _context;
    
        public RealEstateAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(RealEstateAttribute item)
        {
            await _context.RealEstateAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(RealEstateAttribute item)
        {
            _context.RealEstateAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(RealEstateAttribute item)
        {
            _context.RealEstateAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<RealEstateAttribute> GetByIdAsync(Guid id)
        {
            var item = await _context.RealEstateAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<RealEstateAttribute>> GetAllAsync()
        {
            return await _context.RealEstateAttributes.ToListAsync();
        }

        public async Task<IQueryable<RealEstateAttribute>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.RealEstateAttributes.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.RealEstateAttributes.Skip(pageSize * (page - 1))
                                                .Take(pageSize)
                                                .Include(re => re.Product)
                                                    .ThenInclude(p => p.Images)
                                                .Include(re => re.Product)
                                                    .ThenInclude(p => p.City)
                                                .Include(re => re.Product)
                                                    .ThenInclude(p => p.Currency)
                                                .Include(re => re.Product)
                                                    .ThenInclude(p => p.Subcategory)
                                                .Include(re => re.RealEstateType)
                                                .AsQueryable();
        }

        public async Task<RealEstateAttribute> GetDetailByIdAsync(Guid id)
        {
            var item = await _context.RealEstateAttributes.Include(re => re.Product)
                                                            .ThenInclude(p => p.Images)
                                                          .Include(re => re.Product)
                                                            .ThenInclude(p => p.City)
                                                          .Include(re => re.Product)
                                                            .ThenInclude(p => p.Currency)
                                                          .Include(re => re.Product)
                                                            .ThenInclude(p => p.Subcategory)
                                                          .Include(re => re.RealEstateType)
                                                          .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
    }
}
