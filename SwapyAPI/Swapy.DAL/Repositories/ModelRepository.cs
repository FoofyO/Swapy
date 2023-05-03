using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly SwapyDbContext context;

        public ModelRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Model item)
        {
            await context.Models.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Model item)
        {
            context.Models.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Model item)
        {
            context.Models.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Model> GetByIdAsync(Guid id)
        {
            var item = await context.Models.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Model>> GetAllAsync()
        {
            return await context.Models.ToListAsync();
        }
    }
}
