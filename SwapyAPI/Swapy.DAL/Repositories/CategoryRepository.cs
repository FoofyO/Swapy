﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Categories.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
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

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Category> GetByIdAsync(string id)
        {
            var item = await _context.Categories.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<IEnumerable<CategoryTreeResponseDTO>> GetAllAsync(Language language)
        {
            return _context.Categories.Include(s => s.Names)
                                      .AsEnumerable()
                                      .Select(s => new CategoryTreeResponseDTO(s.Id, s.Type, s.Names.FirstOrDefault(l => l.Language == language).Value, false, null, null))
                                      .OrderBy(s => s.Value)
                                      .ToList();
        }
    }
}
