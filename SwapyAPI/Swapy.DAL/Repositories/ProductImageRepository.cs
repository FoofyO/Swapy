﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly SwapyDbContext _context;

        public ProductImageRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ProductImage item)
        {
            await _context.ProductImages.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ProductImage item)
        {
            _context.ProductImages.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ProductImage item)
        {
            _context.ProductImages.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ProductImage> GetByIdAsync(string id)
        {
            var item = await _context.ProductImages.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ProductImage>> GetAllAsync()
        {
            return await _context.ProductImages.ToListAsync();
        }
    }
}
