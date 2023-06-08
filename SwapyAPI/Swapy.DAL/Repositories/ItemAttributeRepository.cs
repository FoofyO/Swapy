using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ItemAttributeRepository : IItemAttributeRepository
    {
        private readonly SwapyDbContext _context;

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public ItemAttributeRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
        }

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

        public async Task<ItemAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.ItemAttributes.Include(a => a.Product)
                                                        .ThenInclude(p => p.Images)
                                                    .Include(x => x.Product)
                                                        .ThenInclude(x => x.Category)
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
                                                                                       bool? isNew,
                                                                                       List<string> itemTypesId,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            var query = _context.ItemAttributes.Include(i => i.Product)
                                                .ThenInclude(p => p.Images)
                                               .Include(i => i.Product)
                                                .ThenInclude(p => p.City)
                                               .Include(i => i.Product)
                                                .ThenInclude(p => p.Currency)
                                               .Include(i => i.Product)
                                                .ThenInclude(p => p.Subcategory)
                                               .Include(i => i.ItemType)
                                               .Where(x => (title == null || x.Product.Title.Contains(title)) &&
                                                     (currencyId == null || x.Product.CurrencyId.Equals(currencyId)) &&
                                                     (priceMin == null || x.Product.Price >= priceMin) &&
                                                     (priceMax == null || x.Product.Price <= priceMax) &&
                                                     (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                                                     (subcategoryId == null || x.Product.SubcategoryId.Equals(subcategoryId)) &&
                                                     (cityId == null || x.Product.CityId.Equals(cityId)) &&
                                                     (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                                                     (isNew == null || x.IsNew == isNew) &&
                                                     x.Product.IsDisable.Equals(false) &&
                                                     (itemTypesId == null || itemTypesId.Contains(x.ItemTypeId)))
                                               .AsQueryable();


            var count = await query.CountAsync();
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (reverseSort == true) query.Reverse();

            query = query.Skip(pageSize * (page - 1))
                 .Take(pageSize);

            var result = await query.Select(x => new ProductResponseDTO()
            {
                Id = x.ProductId,
                Title = x.Product.Title,
                Price = x.Product.Price,
                City = x.Product.City.Name,
                Currency = x.Product.Currency.Name,
                CurrencySymbol = x.Product.Currency.Symbol,
                DateTime = x.Product.DateTime,
                IsDisable = x.Product.IsDisable,
                Images = x.Product.Images.Select(i => i.Image).ToList(),
                UserType = x.Product.User.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize));
        }
    }
}
