﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SwapyDbContext _context;

        public ProductRepository(SwapyDbContext context) => _context = context;

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

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Product> GetByIdAsync(Guid id)
        {
            var item = await _context.Products.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllByUserId(Guid userId)
        {
            return await _context.Products.Where(p => p.UserId == userId).ToListAsync();
        }

        public async Task<IQueryable<Product>> GetByPageAsync(int page = 1, int pageSize = 24)
        {
            if (page < 1 || pageSize < 1) throw new ArgumentException($"Page and page size parameters must be greater than one.");
            if (await _context.Products.CountAsync() <= pageSize * (page - 1)) throw new NotFoundException($"Page {page} not found.");
            return _context.Products.Skip(pageSize * (page - 1))
                                    .Take(pageSize)
                                    .Include(p => p.Images)
                                    .Include(p => p.City)
                                    .Include(p => p.Currency)
                                    .Include(p => p.Subcategory)
                                    .AsQueryable();
        }

        public async Task<Product> GetDetailByIdAsync(Guid id)
        {
            var item = await _context.Products.Include(p => p.Images)
                                              .Include(p => p.City)
                                              .Include(p => p.Currency)
                                              .Include(p => p.Subcategory)
                                              .FirstOrDefaultAsync(a => a.Id == id);

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }
    }
}
