using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoAttributeRepository : IAutoAttributeRepository
    {
        private readonly SwapyDbContext _context;
        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public AutoAttributeRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
        }

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

        public async Task<AutoAttribute> GetByProductIdAsync(string productId)
        {
            var item = await _context.AutoAttributes.Where(x => x.ProductId.Equals(productId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<AutoAttribute>> GetAllAsync()
        {
            return await _context.AutoAttributes.ToListAsync();
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
                                                                                       int? miliageMin,
                                                                                       int? miliageMax,
                                                                                       int? engineCapacityMin,
                                                                                       int? engineCapacityMax,
                                                                                       DateTime? releaseYearOlder,
                                                                                       DateTime? releaseYearNewer,
                                                                                       List<string> fuelTypesId,
                                                                                       List<string> autoColorsId,
                                                                                       List<string> transmissionTypesId,
                                                                                       List<string> autoBrandsId,
                                                                                       List<string> autoTypesId,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            var query = _context.AutoAttributes.Include(a => a.Product)
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
                                               .Include(a => a.AutoModel)
                                                 .ThenInclude(abt => abt.AutoBrand)
                                               .Include(a => a.AutoModel)
                                                 .ThenInclude(abt => abt.AutoType)
                                               .Where(x => (title == null || x.Product.Title.Contains(title)) &&
                                                     (currencyId == null || x.Product.CurrencyId.Equals(currencyId)) &&
                                                     (priceMin == null || x.Product.Price >= priceMin) &&
                                                     (priceMax == null || x.Product.Price <= priceMax) &&
                                                     (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                                                     (subcategoryId == null || x.Product.SubcategoryId.Equals(subcategoryId)) &&
                                                     (cityId == null || x.Product.CityId.Equals(cityId)) &&
                                                     (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                                                     (miliageMin == null || x.Miliage >= miliageMin) &&
                                                     (miliageMax == null || x.Miliage <= miliageMax) &&
                                                     (engineCapacityMin == null || x.EngineCapacity >= engineCapacityMin) &&
                                                     (engineCapacityMax == null || x.EngineCapacity <= engineCapacityMax) &&
                                                     (releaseYearOlder == null || x.ReleaseYear >= releaseYearOlder) &&
                                                     (releaseYearNewer == null || x.ReleaseYear <= releaseYearNewer) &&
                                                     (isNew == null || x.IsNew == isNew) &&
                                                     (fuelTypesId == null || fuelTypesId.Contains(x.FuelTypeId)) &&
                                                     (autoColorsId == null || autoColorsId.Contains(x.AutoColorId)) &&
                                                     (transmissionTypesId == null || transmissionTypesId.Contains(x.TransmissionTypeId)) &&
                                                     (autoBrandsId == null || autoBrandsId.Contains(x.AutoModel.AutoBrandId)) &&
                                                     (autoTypesId == null || autoTypesId.Contains(x.AutoModel.AutoTypeId)))
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
                Images = x.Product.Images.Select(i => i.Image).ToList(),
                UserType = x.Product.User.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(item.Id, userId);
            }

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize));
        }

        public async Task<AutoAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.AutoAttributes.Include(a => a.Product)
                                                        .ThenInclude(p => p.Images)
                                                    .Include(x => x.Product)
                                                        .ThenInclude(x => x.Category)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.City)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.Currency)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.Subcategory)
                                                    .Include(a => a.FuelType)
                                                    .Include(a => a.AutoColor)
                                                    .Include(a => a.TransmissionType)
                                                    .Include(a => a.AutoModel)
                                                        .ThenInclude(abt => abt.AutoBrand)
                                                    .Include(a => a.AutoModel)
                                                        .ThenInclude(abt => abt.AutoType)
                                                    .FirstOrDefaultAsync(a => a.ProductId.Equals(productId));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }
    }
}
