using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ItemAttributeRepository : IItemAttributeRepository
    {
        private readonly SwapyDbContext _context;

        public ItemAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ItemAttribute item)
        {
            await _context.ItemAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ItemAttribute item)
        {
            _context.ItemAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ItemAttribute item)
        {
            _context.ItemAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ItemAttribute> GetByIdAsync(string id)
        {
            var item = await _context.ItemAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<ItemAttribute> GetByProductIdAsync(string productId)
        {
            var item = await _context.ItemAttributes.Where(x => x.ProductId.Equals(productId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<ItemAttribute>> GetAllAsync()
        {
            return await _context.ItemAttributes.ToListAsync();
        }

        public async Task<IQueryable<ItemAttribute>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.ItemAttributes.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.ItemAttributes.Skip(pageSize * (page - 1))
                                          .Take(pageSize)
                                          .Include(i => i.Product)
                                            .ThenInclude(p => p.Images)
                                          .Include(i => i.Product)
                                            .ThenInclude(p => p.City)
                                          .Include(i => i.Product)
                                            .ThenInclude(p => p.Currency)
                                          .Include(i => i.Product)
                                            .ThenInclude(p => p.Subcategory)
                                          .Include(i => i.ItemType)
                                          .AsQueryable();
        }

        public async Task<ItemAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.ItemAttributes.Include(a => a.Product)
                                                        .ThenInclude(p => p.Images)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.City)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.Currency)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.Subcategory)
                                                    .Include(i => i.ItemType)
                                                    .FirstOrDefaultAsync(a => a.ProductId.Equals(productId));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }
    }
}
