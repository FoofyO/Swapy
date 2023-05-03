using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalBreedRepository : IAnimalBreedRepository
    {
        private readonly SwapyDbContext context;

        public AnimalBreedRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(AnimalBreed item)
        {
            await context.AnimalBreeds.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(AnimalBreed item)
        {
            context.AnimalBreeds.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(AnimalBreed item)
        {
            context.AnimalBreeds.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<AnimalBreed> GetByIdAsync(Guid id)
        {
            var item = await context.AnimalBreeds.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<AnimalBreed>> GetAllAsync()
        {
            return await context.AnimalBreeds.ToListAsync();
        }
    }
}
