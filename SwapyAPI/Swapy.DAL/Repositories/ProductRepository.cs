using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SwapyDbContext _context;

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public ProductRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
        }

        public async Task CreateAsync(Product item)
        {
            await _context.Products.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product item)
        {
            _context.Products.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Product item)
        {
            _context.Products.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Product> GetByIdAsync(string id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByUserId(string userId)
        {
            return await _context.Products.Where(p => p.UserId.Equals(userId)).ToListAsync();
        }

        public async Task<Product> GetDetailByIdAsync(string id)
        {
            var item = await _context.Products.Where(a => a.Id.Equals(id))
                                              .Include(p => p.Images)
                                              .Include(p => p.City)
                                                .ThenInclude(c => c.Names)
                                              .Include(p => p.Currency)
                                              .Include(p => p.Subcategory)
                                              .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task IncrementViewsAsync(string id)
        {
            var item = await GetByIdAsync(id);
            item.Views++;
            await UpdateAsync(item);
        }

        public async Task<int> GetProductCountForShopAsync(string userId)
        {
            return await _context.Products.CountAsync(p => p.UserId.Equals(userId));
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
                                                                                       bool? isDisable,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort,
                                                                                       Language language)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            bool isDisableResult = isDisable ?? false;

            if (isDisableResult && (string.IsNullOrEmpty(userId) && string.IsNullOrEmpty(otherUserId))) throw new NoAccessException("No access to get disabled products");

            var query = _context.Products.Include(p => p.Images)
                                         .Include(p => p.Currency)
                                         .Where(x => (title == null || x.Title.Contains(title)) &&
                                               (currencyId == null || x.CurrencyId.Equals(currencyId)) &&
                                               (priceMin == null || x.Price >= priceMin) &&
                                               (priceMax == null || x.Price <= priceMax) &&
                                               (categoryId == null || x.CategoryId.Equals(categoryId)) &&
                                               (subcategoryId == null || x.SubcategoryId.Equals(subcategoryId)) &&
                                               (cityId == null || x.CityId.Equals(cityId)) &&
                                               (otherUserId == null ? !x.UserId.Equals(userId) : x.UserId.Equals(otherUserId)) &&
                                               (isDisable == null || x.IsDisable.Equals(isDisable)))
                                         .AsQueryable();

            var count = await query.CountAsync();
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByPrice == true) query.OrderBy(x => x.Price);
            else query.OrderBy(x => x.DateTime);
            if (reverseSort == true) query.Reverse();

            query.Skip(pageSize * (page - 1))
                 .Take(pageSize)
                 .Include(p => p.Subcategory)
                 .Include(p => p.City)
                    .ThenInclude(c => c.Names);

            var result = await query.Select(x => new ProductResponseDTO()
            {
                Id = x.Id,
                Title = x.Title,
                Price = x.Price,
                City = x.City.Names.FirstOrDefault(l => l.Language == language).Value,
                Currency = x.Currency.Name,
                CurrencySymbol = x.Currency.Symbol,
                DateTime = x.DateTime,
                Images = x.Images.Select(i => i.Image).ToList(),
                UserType = x.User.Type,
                IsDisable = x.IsDisable,
                Type = x.Subcategory.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize));
        }
    }
}