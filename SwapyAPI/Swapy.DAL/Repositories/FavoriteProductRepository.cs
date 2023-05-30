using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class FavoriteProductRepository : IFavoriteProductRepository
    {
        private readonly SwapyDbContext _context;

        public FavoriteProductRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(FavoriteProduct item)
        {
            await _context.FavoriteProducts.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(FavoriteProduct item)
        {
            _context.FavoriteProducts.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(FavoriteProduct item)
        {
            _context.FavoriteProducts.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<FavoriteProduct> GetByIdAsync(string id)
        {
            var item = await _context.FavoriteProducts.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<FavoriteProduct> GetByProductAndUserIdAsync(string productId, string userId)
        {
            var item = await _context.FavoriteProducts.Where(x => x.ProductId.Equals(productId) && x.UserId.Equals(userId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<FavoriteProduct>> GetAllAsync()
        {
            return await _context.FavoriteProducts.ToListAsync();
        }

        public async Task<IQueryable<FavoriteProduct>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.FavoriteProducts.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.FavoriteProducts.Skip(pageSize * (page - 1))
                                    .Take(pageSize)
                                    .Include(fp => fp.Product)
                                        .ThenInclude(p => p.Images)
                                    .Include(fp => fp.Product)
                                        .ThenInclude(p => p.City)
                                    .Include(fp => fp.Product)
                                        .ThenInclude(p => p.Currency)
                                    .Include(fp => fp.Product)
                                        .ThenInclude(fp => fp.Subcategory)
                                    .AsQueryable();
        }

        public async Task<FavoriteProduct> GetDetailByIdAsync(string id)
        {
            var item = await _context.FavoriteProducts.Include(fp => fp.Product)
                                        .ThenInclude(p => p.Images)
                                    .Include(fp => fp.Product)
                                        .ThenInclude(p => p.City)
                                    .Include(fp => fp.Product)
                                        .ThenInclude(p => p.Currency)
                                    .Include(fp => fp.Product)
                                        .ThenInclude(p => p.Subcategory)
                                    .FirstOrDefaultAsync(fp => fp.Id.Equals(id));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
    }
}
