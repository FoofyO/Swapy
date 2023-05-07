﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesSeasonRepository : IClothesSeasonRepository
    {
        private readonly SwapyDbContext _context;

        public ClothesSeasonRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(ClothesSeason item)
        {
            await _context.ClothesSeasons.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesSeason item)
        {
            _context.ClothesSeasons.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesSeason item)
        {
            _context.ClothesSeasons.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<ClothesSeason> GetByIdAsync(Guid id)
        {
            var item = await _context.ClothesSeasons.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<ClothesSeason>> GetAllAsync()
        {
            return await _context.ClothesSeasons.ToListAsync();
        }
    }
}
