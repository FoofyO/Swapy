﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly SwapyDbContext _context;

        public SubcategoryRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Subcategory item)
        {
            await _context.Subcategories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Subcategory item)
        {
            _context.Subcategories.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Subcategory item)
        {
            _context.Subcategories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Subcategory> GetByIdAsync(string id)
        {
            var item = await _context.Subcategories.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<Subcategory> GetDetailByIdAsync(string id)
        {
            var item = await _context.Subcategories.Include(s => s.ChildSubcategories)
                                                    .ThenInclude(b => b.Child)
                                                   .Include(s => s.ParentSubcategory)
                                                    .ThenInclude(b => b.Parent)
                                                   .FirstOrDefaultAsync(s => s.Id.Equals(id));

            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Subcategory>> GetAllAsync()
        {
            return await _context.Subcategories.ToListAsync();
        }

        public async Task<IEnumerable<Subcategory>> GetByCategoryAsync(string categoryId)
        {
            var item = await _context.Subcategories.Where(s => s.CategoryId.Equals(categoryId) && s.ParentSubcategoryId == null).ToListAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {categoryId} id not found");
            return item;
        }

        public async Task<IEnumerable<Subcategory>> GetBySubcategoryAsync(string subcategoryId)
        {
            var item = await _context.Subcategories.Where(s => s.ParentSubcategoryId.Equals(subcategoryId)).ToListAsync();
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {subcategoryId} id not found");
            return item;
        }
    }
}
