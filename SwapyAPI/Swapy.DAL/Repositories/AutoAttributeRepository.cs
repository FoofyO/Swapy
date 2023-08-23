using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Autos.Responses;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoAttributeRepository : IAutoAttributeRepository
    {
        private readonly SwapyDbContext _context;

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        private readonly ISubcategoryRepository _subcategoryRepository;

        public AutoAttributeRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository, ISubcategoryRepository subcategoryRepository)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
            _subcategoryRepository = subcategoryRepository;
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

        public async Task<AutoAttributesResponseDTO> GetAllFilteredAsync(int page,
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
                                                                                       bool? reverseSort,
                                                                                       Language language)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            List<SpecificationResponseDTO<string>> sequenceOfSubcategories = subcategoryId == null ? new() : (await _subcategoryRepository.GetSequenceOfSubcategories(subcategoryId, language)).ToList();

            var query = _context.AutoAttributes.Include(a => a.Product)
                                                   .ThenInclude(p => p.Images)
                                               .Include(a => a.Product)
                                                   .ThenInclude(p => p.Currency)
                                               .Include(a => a.AutoModel)
                                               .AsQueryable();

            decimal minPrice = await query.Select(x => x.Product.Price).OrderBy(p => p).FirstOrDefaultAsync();
            decimal maxPrice = await query.Select(x => x.Product.Price).OrderBy(p => p).LastOrDefaultAsync();

            int minMiliage = await query.Select(x => x.Miliage).OrderBy(p => p).FirstOrDefaultAsync();
            int maxMiliage = await query.Select(x => x.Miliage).OrderBy(p => p).LastOrDefaultAsync();

            int minEngineCapacity = await query.Select(x => x.EngineCapacity).OrderBy(p => p).FirstOrDefaultAsync();
            int maxEngineCapacity = await query.Select(x => x.EngineCapacity).OrderBy(p => p).LastOrDefaultAsync();

            int olderReleaseYear = Convert.ToInt32((await query.Select(x => x.ReleaseYear).OrderBy(p => p).FirstOrDefaultAsync()).Year.ToString());
            int newerReleaseYear = Convert.ToInt32((await query.Select(x => x.ReleaseYear).OrderBy(p => p).LastOrDefaultAsync()).Year.ToString());

            query = query.Where(x => (priceMin == null || x.Product.Price >= priceMin) &&
                    (priceMax == null || x.Product.Price <= priceMax) && 
                    (miliageMin == null || x.Miliage >= miliageMin) &&
                    (miliageMax == null || x.Miliage <= miliageMax) &&
                    (engineCapacityMin == null || x.EngineCapacity >= engineCapacityMin) &&
                    (engineCapacityMax == null || x.EngineCapacity <= engineCapacityMax) &&
                    (releaseYearOlder == null || x.ReleaseYear >= releaseYearOlder) &&
                    (releaseYearNewer == null || x.ReleaseYear <= releaseYearNewer) &&
                    (title == null || x.Product.Title.Contains(title)) &&
                    (currencyId == null || x.Product.CurrencyId.Equals(currencyId)) &&
                    (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                    (subcategoryId == null ? true : sequenceOfSubcategories.Select(x => x.Id).Contains(subcategoryId)) &&
                    (cityId == null || x.Product.CityId.Equals(cityId)) &&
                    (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                    (isNew == null || x.IsNew == isNew) &&
                    x.Product.IsDisable.Equals(false) &&
                    (fuelTypesId == null || fuelTypesId.Contains(x.FuelTypeId)) &&
                    (autoColorsId == null || autoColorsId.Contains(x.AutoColorId)) &&
                    (transmissionTypesId == null || transmissionTypesId.Contains(x.TransmissionTypeId)) &&
                    (autoBrandsId == null || autoBrandsId.Contains(x.AutoModel.AutoBrandId)) &&
                    (autoTypesId == null || autoTypesId.Contains(x.AutoModel.AutoTypeId)));

            var count = await query.CountAsync();
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByPrice == true) query = query.OrderBy(x => x.Product.Price);
            else query = query.OrderBy(x => x.Product.DateTime);
            if (reverseSort == true) query = query.Reverse();

            query = query.Skip(pageSize * (page - 1))
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
                UserId = x.Product.UserId,
                Type = x.Product.Subcategory.Type
            }).ToListAsync();

            foreach (var item in result)
            {
                item.IsFavorite = await _favoriteProductRepository.CheckProductOnFavorite(item.Id, userId);
            }

            return new AutoAttributesResponseDTO(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize), maxPrice, minPrice, maxMiliage, minMiliage, maxEngineCapacity, minEngineCapacity, olderReleaseYear, newerReleaseYear);
        }

        public async Task<AutoAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.AutoAttributes.Where(a => a.ProductId.Equals(productId))
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.Images)
                                                    .Include(x => x.Product)
                                                        .ThenInclude(x => x.Category)
                                                            .ThenInclude(c => c.Names)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.City)
                                                            .ThenInclude(c => c.Names)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.Currency)
                                                    .Include(a => a.Product)
                                                        .ThenInclude(p => p.User)
                                                            .ThenInclude(u => u.ShopAttribute)
                                                    .Include(a => a.FuelType)
                                                        .ThenInclude(ft => ft.Names)
                                                    .Include(a => a.AutoColor)
                                                        .ThenInclude(ac => ac.Names)
                                                    .Include(a => a.TransmissionType)
                                                        .ThenInclude(tt => tt.Names)
                                                    .Include(a => a.AutoModel)
                                                        .ThenInclude(am => am.AutoBrand)
                                                    .Include(a => a.AutoModel)
                                                        .ThenInclude(am => am.AutoType)
                                                            .ThenInclude(at => at.Names)
                                                    .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }
    }
}
