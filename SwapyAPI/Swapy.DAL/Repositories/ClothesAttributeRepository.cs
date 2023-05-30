using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesAttributeRepository : IClothesAttributeRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesAttributeRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesAttribute item)
        {
            await _context.ClothesAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesAttribute item)
        {
            _context.ClothesAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesAttribute item)
        {
            _context.ClothesAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ClothesAttribute> GetByIdAsync(string id)
        {
            var item = await _context.ClothesAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<ClothesAttribute> GetByProductIdAsync(string productId)
        {
            var item = await _context.ClothesAttributes.Where(x => x.ProductId.Equals(productId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<ClothesAttribute>> GetAllAsync()
        {
            return await _context.ClothesAttributes.ToListAsync();
        }

        public async Task<IQueryable<ClothesAttribute>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.ClothesAttributes.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.ClothesAttributes.Skip(pageSize * (page - 1))
                                             .Take(pageSize)
                                             .Include(c => c.Product)
                                                .ThenInclude(p => p.Images)
                                             .Include(c => c.Product)
                                                .ThenInclude(p => p.City)
                                             .Include(c => c.Product)
                                                .ThenInclude(p => p.Currency)
                                             .Include(c => c.Product)
                                                .ThenInclude(p => p.Subcategory)
                                             .Include(c => c.ClothesSize)
                                             .Include(c => c.ClothesSeason)
                                             .Include(c => c.ClothesBrandView)
                                                .ThenInclude(cbv => cbv.ClothesBrand)
                                             .Include(c => c.ClothesBrandView)
                                                .ThenInclude(cbv => cbv.ClothesView)
                                                    .ThenInclude(cv => cv.ClothesType)
                                             .Include(c => c.ClothesBrandView)
                                                .ThenInclude(cbv => cbv.ClothesView)
                                                    .ThenInclude(cv => cv.Gender)
                                             .AsQueryable();
        }

        public async Task<ClothesAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.ClothesAttributes.Include(c => c.Product)
                                                        .ThenInclude(p => p.Images)
                                                       .Include(c => c.Product)
                                                        .ThenInclude(p => p.City)
                                                       .Include(c => c.Product)
                                                        .ThenInclude(p => p.Currency)
                                                       .Include(c => c.Product)
                                                        .ThenInclude(p => p.Subcategory)
                                                       .Include(c => c.ClothesSize)
                                                       .Include(c => c.ClothesSeason)
                                                       .Include(c => c.ClothesBrandView)
                                                        .ThenInclude(cbv => cbv.ClothesBrand)
                                                       .Include(c => c.ClothesBrandView)
                                                        .ThenInclude(cbv => cbv.ClothesView)
                                                            .ThenInclude(cv => cv.ClothesType)
                                                       .Include(c => c.ClothesBrandView)
                                                        .ThenInclude(cbv => cbv.ClothesView)
                                                            .ThenInclude(cv => cv.Gender)
                                                       .FirstOrDefaultAsync(a => a.ProductId.Equals(productId));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }
    }
}
 