using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
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

        public async Task<FavoriteProduct> GetDetailByIdAsync(string id)
        {
            var item = await _context.FavoriteProducts.Where(fp => fp.Id.Equals(id))
                                                      .Include(fp => fp.Product)
                                                        .ThenInclude(p => p.Images)
                                                      .Include(fp => fp.Product)
                                                        .ThenInclude(p => p.City)
                                                            .ThenInclude(c => c.Names)
                                                      .Include(fp => fp.Product)
                                                        .ThenInclude(p => p.Currency)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.User)
                                                            .ThenInclude(u => u.ShopAttribute)
                                                      .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<ProductsResponseDTO<ProductResponseDTO>> GetAllFilteredAsync(int page,
                                                                                       int pageSize,
                                                                                       string userId,
                                                                                       string title,
                                                                                       string currencyId,
                                                                                       decimal? priceMin,
                                                                                       decimal? priceMax,
                                                                                       string categoryId,
                                                                                       string subcategoryId,
                                                                                       string cityId,
                                                                                       string otherUserId,
                                                                                       string productId,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort,
                                                                                       Language language)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            var query = _context.FavoriteProducts.Include(fp => fp.Product)
                                                    .ThenInclude(p => p.Images)
                                                 .Include(fp => fp.Product)
                                                    .ThenInclude(p => p.Currency)
                                                 .Where(x => (title == null || x.Product.Title.Contains(title)) &&
                                                       (currencyId == null || x.Product.CurrencyId.Equals(currencyId)) &&
                                                       (priceMin == null || x.Product.Price >= priceMin) &&
                                                       (priceMax == null || x.Product.Price <= priceMax) &&
                                                       (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                                                       (subcategoryId == null || x.Product.SubcategoryId.Equals(subcategoryId)) &&
                                                       (cityId == null || x.Product.CityId.Equals(cityId)) &&
                                                       (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                                                       (productId == null || x.ProductId.Equals(productId)))
                                                 .AsQueryable();

            decimal maxPrice = await query.Select(x => x.Product.Price).OrderBy(p => p).FirstOrDefaultAsync();
            decimal minPrice = await query.Select(x => x.Product.Price).OrderBy(p => p).LastOrDefaultAsync();

            var count = await query.CountAsync();
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (reverseSort == true) query.Reverse();

            query.Skip(pageSize * (page - 1))
                 .Take(pageSize)
                 .Include(a => a.Product)
                    .ThenInclude(p => p.Subcategory)
                 .Include(a => a.Product)
                    .ThenInclude(p => p.City)
                        .ThenInclude(c => c.Names);

            var result = await query.Select(x => new ProductResponseDTO()
            {
                Id = x.ProductId,
                Title = x.Product.Title,
                Price = x.Product.Price,
                City = x.Product.City.Names.FirstOrDefault(l => l.Language == language).Value,
                Currency = x.Product.Currency.Name,
                CurrencySymbol = x.Product.Currency.Symbol,
                DateTime = x.Product.DateTime,
                IsDisable = x.Product.IsDisable,
                Images = x.Product.Images.Select(i => i.Image).ToList(),
                UserType = x.Product.User.Type,
                Type = x.Product.Subcategory.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize), maxPrice, minPrice);
        }

        public async Task<bool> CheckProductOnFavorite(string productId, string userId)
        {
            var item = await _context.FavoriteProducts.Where(x => x.ProductId.Equals(productId) && x.UserId.Equals(userId)).FirstOrDefaultAsync();
            if (item == null) return false;
            return true;
        }
    }
}
