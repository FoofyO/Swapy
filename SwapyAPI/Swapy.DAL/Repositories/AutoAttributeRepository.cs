using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoAttributeRepository : IAutoAttributeRepository
    {
        private readonly SwapyDbContext _context;
    
        public AutoAttributeRepository(SwapyDbContext context) => _context = context;
        
        public async Task CreateAsync(AutoAttribute item)
        {
            await _context.AutoAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AutoAttribute item)
        {
            _context.AutoAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AutoAttribute item)
        {
            _context.AutoAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AutoAttribute> GetByIdAsync(string id)
        {
            var item = await _context.AutoAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<AutoAttribute>> GetAllAsync()
        {
            return await _context.AutoAttributes.ToListAsync();
        }

        public async Task<IQueryable<AutoAttribute>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.AutoAttributes.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.AutoAttributes.Skip(pageSize * (page - 1))
                                          .Take(pageSize)
                                          .Include(a => a.Product)
                                            .ThenInclude(p => p.Images)
                                          .Include(a => a.Product)
                                            .ThenInclude(p => p.City)
                                          .Include(a => a.Product)
                                            .ThenInclude(p => p.Currency)
                                          .Include(a => a.Product)
                                            .ThenInclude(p => p.Subcategory)
                                          .Include(a => a.FuelType)
                                          .Include(a => a.AutoColor)
                                          .Include(a => a.TransmissionType)
                                          .Include(a => a.AutoBrandType)
                                            .ThenInclude(abt => abt.AutoBrand)
                                          .Include(a => a.AutoBrandType)
                                            .ThenInclude(abt => abt.AutoType)
                                          .AsQueryable();
        }

        public async Task<AutoAttribute> GetDetailByIdAsync(string id)
        {
            var item = await _context.AutoAttributes.Include(a => a.Product)
                                                        .ThenInclude(p => p.Images)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.City)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.Currency)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.Subcategory)
                                                    .Include(a => a.FuelType)
                                                    .Include(a => a.AutoColor)
                                                    .Include(a => a.TransmissionType)
                                                    .Include(a => a.AutoBrandType)
                                                        .ThenInclude(abt => abt.AutoBrand)
                                                    .Include(a => a.AutoBrandType)
                                                        .ThenInclude(abt => abt.AutoType)
                                                    .FirstOrDefaultAsync(a => a.Id.Equals(id));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
    }
}
