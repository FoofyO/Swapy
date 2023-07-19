﻿using Microsoft.EntityFrameworkCore;
using Swapy.Common.DTO.Products.Responses;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly SwapyDbContext _context;

        public CityRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(City item)
        {
            await _context.Cities.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(City item)
        {
            _context.Cities.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(City item)
        {
            _context.Cities.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(string id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<City> GetByIdAsync(string id)
        {
            var item = await _context.Cities.FindAsync(id);
            if (item == null) throw new NotFoundException($"{GetType().Name.Split("Repository")[0]} with {id} id not found");
            return item;
        }

        public async Task<IEnumerable<City>> GetAllAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<IEnumerable<SpecificationResponseDTO<string>>> GetAllAsync(Language language)
        {
            return _context.Cities.Include(s => s.Names)
                                  .AsEnumerable()
                                  .Select(s => new SpecificationResponseDTO<string>(s.Id, s.Names.FirstOrDefault(l => l.Language == language).Value))
                                  .OrderBy(s => s.Value)
                                  .ToList();
        }

        public async Task<string> GetLocalizeByIdAsync(string cityId, Language language)
        {
            var city = await _context.Cities.Where(c => c.Id.Equals(cityId))
                                            .Include(s => s.Names)
                                            .FirstOrDefaultAsync();

            return city.Names.FirstOrDefault(l => l.Language == language).Value;
        }
    }
}
