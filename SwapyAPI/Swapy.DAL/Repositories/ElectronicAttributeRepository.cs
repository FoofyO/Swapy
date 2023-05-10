using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
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

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ElectronicAttribute> GetByIdAsync(string id)
        {
            var item = await _context.ElectronicAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ElectronicAttribute>> GetAllAsync()
        {
            return await _context.ElectronicAttributes.ToListAsync();
        }

        public async Task<IQueryable<ElectronicAttribute>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.ElectronicAttributes.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.ElectronicAttributes.Skip(pageSize * (page - 1))
                                                .Take(pageSize)
                                                .Include(e => e.Product)
                                                    .ThenInclude(p => p.Images)
                                                .Include(e => e.Product)
                                                    .ThenInclude(p => p.City)
                                                .Include(e => e.Product)
                                                    .ThenInclude(p => p.Currency)
                                                .Include(e => e.Product)
                                                    .ThenInclude(p => p.Subcategory)
                                                .Include(e => e.MemoryModel)
                                                    .ThenInclude(mm => mm.Memory)
                                                .Include(e => e.MemoryModel)
                                                    .ThenInclude(mm => mm.Model)
                                                        .ThenInclude(m => m.ElectronicBrandType)
                                                            .ThenInclude(bt => bt.ElectronicBrand)
                                                .Include(e => e.MemoryModel)
                                                    .ThenInclude(mm => mm.Model)
                                                        .ThenInclude(m => m.ElectronicBrandType)
                                                            .ThenInclude(bt => bt.ElectronicType)
                                                .Include(e => e.ModelColor)
                                                    .ThenInclude(mm => mm.Color)
                                                .Include(e => e.ModelColor)
                                                    .ThenInclude(mm => mm.Model)
                                                        .ThenInclude(m => m.ElectronicBrandType)
                                                            .ThenInclude(bt => bt.ElectronicBrand)
                                                .Include(e => e.ModelColor)
                                                    .ThenInclude(mm => mm.Model)
                                                        .ThenInclude(m => m.ElectronicBrandType)
                                                            .ThenInclude(bt => bt.ElectronicType)
                                                .AsQueryable();
        }

        public async Task<ElectronicAttribute> GetDetailByIdAsync(string id)
        {
            var item = await _context.ElectronicAttributes.Include(a => a.Product)
                                                            .ThenInclude(p => p.Images)
                                                          .Include(a => a.Product)
                                                            .ThenInclude(p => p.City)
                                                          .Include(a => a.Product)
                                                            .ThenInclude(p => p.Currency)
                                                          .Include(a => a.Product)
                                                            .ThenInclude(p => p.Subcategory)
                                                          .Include(e => e.MemoryModel)
                                                            .ThenInclude(mm => mm.Memory)
                                                          .Include(e => e.MemoryModel)
                                                            .ThenInclude(mm => mm.Model)
                                                                .ThenInclude(m => m.ElectronicBrandType)
                                                                    .ThenInclude(bt => bt.ElectronicBrand)
                                                          .Include(e => e.MemoryModel)
                                                            .ThenInclude(mm => mm.Model)
                                                                .ThenInclude(m => m.ElectronicBrandType)
                                                                    .ThenInclude(bt => bt.ElectronicType)
                                                          .Include(e => e.ModelColor)
                                                            .ThenInclude(mm => mm.Color)
                                                          .Include(e => e.ModelColor)
                                                            .ThenInclude(mm => mm.Model)
                                                                .ThenInclude(m => m.ElectronicBrandType)
                                                                    .ThenInclude(bt => bt.ElectronicBrand)
                                                          .Include(e => e.ModelColor)
                                                            .ThenInclude(mm => mm.Model)
                                                                .ThenInclude(m => m.ElectronicBrandType)
                                                                    .ThenInclude(bt => bt.ElectronicType)
                                                          .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
    }
}
