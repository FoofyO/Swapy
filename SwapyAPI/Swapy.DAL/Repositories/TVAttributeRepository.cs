using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVAttributeRepository : ITVAttributeRepository
    {
        private readonly SwapyDbContext _context;

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        private readonly ISubcategoryRepository _subcategoryRepository;

        public TVAttributeRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository, ISubcategoryRepository subcategoryRepository)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
            _subcategoryRepository = subcategoryRepository;
        }

        public async Task CreateAsync(TVAttribute item)
        {
            await _context.TVAttributes.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TVAttribute item)
        {
            _context.TVAttributes.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TVAttribute item)
        {
            _context.TVAttributes.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<TVAttribute> GetByIdAsync(string id)
        {
            var item = await _context.TVAttributes.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<TVAttribute> GetByProductIdAsync(string productId)
        {
            var item = await _context.TVAttributes.Where(x => x.ProductId.Equals(productId)).FirstOrDefaultAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {productId} id not found");
            return item;
        }

        public async Task<IEnumerable<TVAttribute>> GetAllAsync()
        {
            return await _context.TVAttributes.ToListAsync();
        }

        public async Task<TVAttribute> GetDetailByIdAsync(string productId)
        {
            var item = await _context.TVAttributes.Include(tv => tv.Product)
                                                    .ThenInclude(p => p.Images)
                                                   .Include(x => x.Product)
                                                    .ThenInclude(x => x.Category)
                                                  .Include(tv => tv.Product)
                                                    .ThenInclude(p => p.City)
                                                        .ThenInclude(c => c.Names)
                                                  .Include(tv => tv.Product)
                                                    .ThenInclude(p => p.Currency)
                                                  .Include(tv => tv.Product)
                                                    .ThenInclude(p => p.Subcategory)
                                                  .Include(a => a.Product)
                                                    .ThenInclude(p => p.User)
                                                        .ThenInclude(u => u.ShopAttribute)
                                                  .Include(tv => tv.TVBrand)
                                                  .Include(tv => tv.ScreenResolution)
                                                  .Include(tv => tv.ScreenDiagonal)
                                                  .Include(tv => tv.TVType)
                                                    .ThenInclude(t => t.Names)
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
                                                                                       bool? isSmart,
                                                                                       List<string> tvTypesId,
                                                                                       List<string> tvBrandsId,
                                                                                       List<string> screenResolutionsId,
                                                                                       List<string> screenDiagonalsId,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort,
                                                                                       Language language)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            List<SpecificationResponseDTO<string>> sequenceOfSubcategories = subcategoryId == null ? new() : (await _subcategoryRepository.GetSequenceOfSubcategories(subcategoryId, language)).ToList();

            var query = _context.TVAttributes.Include(tv => tv.Product)
                                                .ThenInclude(p => p.Images)
                                             .Include(tv => tv.Product)
                                                .ThenInclude(p => p.City)
                                             .Include(tv => tv.Product)
                                                .ThenInclude(p => p.Currency)
                                             .AsQueryable();

            decimal maxPrice = await query.Select(x => x.Product.Price).OrderBy(p => p).FirstOrDefaultAsync();
            decimal minPrice = await query.Select(x => x.Product.Price).OrderBy(p => p).LastOrDefaultAsync();

            query = query.Where(x => (priceMin == null || x.Product.Price >= priceMin) &&
                    (priceMax == null || x.Product.Price <= priceMax) &&
                    (title == null || x.Product.Title.Contains(title)) &&
                    (currencyId == null || x.Product.CurrencyId.Equals(currencyId)) &&
                    (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                    (subcategoryId == null ? true : sequenceOfSubcategories.Select(x => x.Id).Contains(subcategoryId)) &&
                    (cityId == null || x.Product.CityId.Equals(cityId)) &&
                    (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                    (isNew == null || x.IsNew == isNew) &&
                    (isSmart == null || x.IsSmart == isSmart) &&
                    x.Product.IsDisable.Equals(false) &&
                    (tvTypesId == null || tvTypesId.Equals(x.TVTypeId)) &&
                    (tvBrandsId == null || tvBrandsId.Contains(x.TVBrandId)) &&
                    (screenResolutionsId == null || screenResolutionsId.Contains(x.ScreenResolutionId)) &&
                    (screenDiagonalsId == null || screenDiagonalsId.Contains(x.ScreenDiagonalId)));

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
    }
} 