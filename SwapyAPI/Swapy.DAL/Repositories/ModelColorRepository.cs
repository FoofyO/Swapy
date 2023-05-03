using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ModelColorRepository : IModelColorRepository
    {
        private readonly SwapyDbContext context;

        public ModelColorRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ModelColor item)
        {
            await context.ModelsColors.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ModelColor item)
        {
            context.ModelsColors.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ModelColor item)
        {
            context.ModelsColors.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ModelColor> GetByIdAsync(Guid id)
        {
            var item = await context.ModelsColors.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ModelColor>> GetAllAsync()
        {
            return await context.ModelsColors.ToListAsync();
        }
    }
}
