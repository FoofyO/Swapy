using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicAttributeRepository : IElectronicAttributeRepository
    {
        private readonly SwapyDbContext _context;

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        private readonly ISubcategoryRepository _subcategoryRepository;

        public ElectronicAttributeRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository, ISubcategoryRepository subcategoryRepository)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
            _subcategoryRepository = subcategoryRepository;
        }

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

        public async Task<ElectronicAttribute> GetByProductIdAsync(string productId)
        {
            var item = await _context.ElectronicAttributes.Where(x => x.ProductId.Equals(productId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<ElectronicAttribute>> GetAllAsync()
        {
            return await _context.ElectronicAttributes.ToListAsync();
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
                                                                               List<string> memoriesId,
                                                                               List<string> colorsId,
                                                                               List<string> modelsId,
                                                                               List<string> brandsId,
                                                                               List<string> typesId,
                                                                               bool? sortByPrice,
                                                                               bool? reverseSort,
                                                                               Language language)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            List<SpecificationResponseDTO<string>> sequenceOfSubcategories = subcategoryId == null ? new() : (await _subcategoryRepository.GetSequenceOfSubcategories(subcategoryId, language)).ToList();

            var query = _context.ElectronicAttributes.Include(e => e.Product)
                                                        .ThenInclude(p => p.Images)
                                                     .Include(e => e.Product)
                                                        .ThenInclude(p => p.Currency)
                                                     .Include(e => e.MemoryModel)
                                                        .ThenInclude(mm => mm.Model)
                                                            .ThenInclude(m => m.ElectronicBrandType)
                                                     .Include(e => e.ModelColor)
                                                     .AsQueryable();

            decimal minPrice = await query.Select(x => x.Product.Price).OrderBy(p => p).FirstOrDefaultAsync();
            decimal maxPrice = await query.Select(x => x.Product.Price).OrderBy(p => p).LastOrDefaultAsync();

            query = query.Where(x => (priceMin == null || x.Product.Price >= priceMin) &&
                    (priceMax == null || x.Product.Price <= priceMax) &&
                    (title == null || x.Product.Title.Contains(title)) &&
                    (currencyId == null || x.Product.CurrencyId.Equals(currencyId)) &&
                    (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                    (subcategoryId == null ? true : sequenceOfSubcategories.Select(x => x.Id).Contains(subcategoryId)) &&
                    (cityId == null || x.Product.CityId.Equals(cityId)) &&
                    (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                    (isNew == null || x.IsNew == isNew) &&
                    x.Product.IsDisable.Equals(false) &&
                    (memoriesId == null || memoriesId.Contains(x.MemoryModel.MemoryId)) &&
                    (colorsId == null || colorsId.Contains(x.ModelColor.ColorId)) &&
                    (modelsId == null || modelsId.Contains(x.MemoryModel.ModelId)) &&
                    (brandsId == null && modelsId != null || brandsId.Contains(x.MemoryModel.Model.ElectronicBrandType.ElectronicBrandId)) &&
                    (typesId == null && modelsId != null || typesId.Contains(x.MemoryModel.Model.ElectronicBrandType.ElectronicTypeId)));

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

            return new ProductsResponseDTO<ProductResponseDTO>(result, count, (int)Math.Ceiling(Convert.ToDouble(count) / pageSize), maxPrice, minPrice);
        }

        public async Task<ElectronicAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.ElectronicAttributes.Where(a => a.ProductId.Equals(productId))
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
                                                          .Include(e => e.MemoryModel)
                                                            .ThenInclude(mm => mm.Memory)
                                                          .Include(e => e.MemoryModel)
                                                            .ThenInclude(mm => mm.Model)
                                                                .ThenInclude(m => m.ElectronicBrandType)
                                                                    .ThenInclude(bt => bt.ElectronicBrand)
                                                          .Include(e => e.MemoryModel)
                                                            .ThenInclude(mm => mm.Model)
                                                                .ThenInclude(m => m.ElectronicBrandType)
                                                                    .ThenInclude(ebt => ebt.ElectronicType)
                                                                        .ThenInclude(et => et.Names)
                                                          .Include(e => e.ModelColor)
                                                            .ThenInclude(mm => mm.Color)
                                                                .ThenInclude(c => c.Names)
                                                          .FirstOrDefaultAsync();

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }
    }
}
