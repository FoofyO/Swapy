﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly SwapyDbContext _context;

        public CategoryRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Category item)
        {
            await _context.Categories.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Category item)
        {
            _context.Categories.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Category item)
        {
            _context.Categories.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Category> GetByIdAsync(Guid id)
        {
            var item = await _context.Categories.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
