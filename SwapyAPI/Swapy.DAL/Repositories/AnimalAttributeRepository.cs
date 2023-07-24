using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalAttributeRepository : IAnimalAttributeRepository
    {
        private readonly SwapyDbContext _context;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public AnimalAttributeRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
        }
        
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

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AnimalAttribute> GetByIdAsync(string id)
        {
            var item = await _context.AnimalAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<AnimalAttribute> GetByProductIdAsync(string productId)
        {
            var item = await _context.AnimalAttributes.Where(x => x.ProductId.Equals(productId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<AnimalAttribute>> GetAllAsync()
        {
            return await _context.AnimalAttributes.ToListAsync();
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
                                                                                       List<string> animalBreedsId,
                                                                                       List<string> animalTypesId,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort,
                                                                                       Language language)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            var query = _context.AnimalAttributes.Include(a => a.Product)
                                                    .ThenInclude(p => p.Images)
                                                 .Include(a => a.Product)
                                                    .ThenInclude(p => p.Currency)
                                                 .Include(a => a.AnimalBreed)
                                                 .Where(x => (title == null || x.Product.Title.Contains(title)) &&
                                                       (currencyId == null || x.Product.CurrencyId.Equals(currencyId)) &&
                                                       (priceMin == null || x.Product.Price >= priceMin) &&
                                                       (priceMax == null || x.Product.Price <= priceMax) &&
                                                       (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                                                       (subcategoryId == null || x.Product.SubcategoryId.Equals(subcategoryId)) &&
                                                       (cityId == null || x.Product.CityId.Equals(cityId)) &&
                                                       (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                                                       (animalBreedsId == null || animalBreedsId.Contains(x.AnimalBreedId)) &&
                                                       x.Product.IsDisable.Equals(false) &&
                                                       (animalTypesId == null || animalTypesId.Contains(x.AnimalBreed.AnimalTypeId)))
                                                 .AsQueryable();

            var count = await query.CountAsync();
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (reverseSort == true) query.Reverse();

            query = query.Skip(pageSize * (page - 1))
                 .Take(pageSize)
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
                Images = x.Product.Images.Select(i => i.Image).ToList(),
                IsDisable = x.Product.IsDisable,
                UserType = x.Product.User.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize));
        }

        public async Task<AnimalAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.AnimalAttributes.Where(a => a.ProductId.Equals(productId))
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.Images)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.Category)
                                                            .ThenInclude(c => c.Names)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.City)
                                                            .ThenInclude(c => c.Names)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.Currency)
                                                      .Include(a => a.Product)
                                                        .ThenInclude(p => p.User)
                                                            .ThenInclude(u => u.ShopAttribute)
                                                      .Include(a => a.AnimalBreed)
                                                        .ThenInclude(ab => ab.Names)
                                                      .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }
    }
}
