﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesAttributeRepository : IClothesAttributeRepository
    {
        private readonly SwapyDbContext _context;

        private readonly IFavoriteProductRepository _favoriteProductRepository;

        public ClothesAttributeRepository(SwapyDbContext context, IFavoriteProductRepository favoriteProductRepository)
        {
            _context = context;
            _favoriteProductRepository = favoriteProductRepository;
        }

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
                                                                                       List<string> clothesSeasonsId,
                                                                                       List<string> clothesSizesId,
                                                                                       List<string> clothesBrandsId,
                                                                                       List<string> clothesViewsId,
                                                                                       List<string> clothesTypesId,
                                                                                       List<string> clothesGendersId,
                                                                                       bool? sortByPrice,
                                                                                       bool? reverseSort)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");

            var query = _context.ClothesAttributes.Include(c => c.Product)
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
                                                 .Where(x => (title == null || x.Product.Title.Contains(title)) &&
                                                       (currencyId == null || x.Product.CurrencyId.Equals(currencyId)) &&
                                                       (priceMin == null || x.Product.Price >= priceMin) &&
                                                       (priceMax == null || x.Product.Price <= priceMax) &&
                                                       (categoryId == null || x.Product.CategoryId.Equals(categoryId)) &&
                                                       (subcategoryId == null || x.Product.SubcategoryId.Equals(subcategoryId)) &&
                                                       (cityId == null || x.Product.CityId.Equals(cityId)) &&
                                                       (otherUserId == null ? !x.Product.UserId.Equals(userId) : x.Product.UserId.Equals(otherUserId)) &&
                                                       (isNew == null || x.IsNew == isNew) &&
                                                       (clothesSeasonsId == null || clothesSeasonsId.Contains(x.ClothesSeasonId)) &&
                                                       (clothesSizesId == null || clothesSizesId.Contains(x.ClothesSizeId)) &&
                                                       (clothesBrandsId == null || clothesBrandsId.Contains(x.ClothesBrandView.ClothesBrandId)) &&
                                                       (clothesViewsId == null || clothesViewsId.Contains(x.ClothesBrandView.ClothesViewId)) &&
                                                       (clothesTypesId == null && clothesViewsId != null || clothesTypesId.Contains(x.ClothesBrandView.ClothesView.ClothesTypeId)) &&
                                                       (clothesGendersId == null && clothesViewsId != null || clothesGendersId.Contains(x.ClothesBrandView.ClothesView.GenderId)))
                                                 .AsQueryable();

            var count = await query.CountAsync();
            if (count <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");

            if (sortByPrice == true) query.OrderBy(x => x.Product.Price);
            else query.OrderBy(x => x.Product.DateTime);
            if (reverseSort == true) query.Reverse();

            query.Skip(pageSize * (page - 1))
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
 