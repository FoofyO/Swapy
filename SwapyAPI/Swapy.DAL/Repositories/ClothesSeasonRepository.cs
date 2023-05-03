using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesSeasonRepository : IClothesSeasonRepository
    {
        private readonly SwapyDbContext context;

        public ClothesSeasonRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ClothesSeason item)
        {
            await context.ClothesSeasons.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ClothesSeason item)
        {
            context.ClothesSeasons.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ClothesSeason item)
        {
            context.ClothesSeasons.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ClothesSeason> GetByIdAsync(Guid id)
        {
            var item = await context.ClothesSeasons.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ClothesSeason>> GetAllAsync()
        {
            return await context.ClothesSeasons.ToListAsync();
        }
    }
}
