using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalBreedRepository : IAnimalBreedRepository
    {
        private readonly SwapyDbContext _context;

        public AnimalBreedRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(AnimalBreed item)
        {
            await _context.AnimalBreeds.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalBreed item)
        {
            _context.AnimalBreeds.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AnimalBreed item)
        {
            _context.AnimalBreeds.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<AnimalBreed> GetByIdAsync(Guid id)
        {
            var item = await _context.AnimalBreeds.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AnimalBreed>> GetAllAsync()
        {
            return await _context.AnimalBreeds.ToListAsync();
        }
    }
}
