using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ItemAttributeRepository : IItemAttributeRepository
    {
        private readonly SwapyDbContext context;

        public ItemAttributeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ItemAttribute item)
        {
            await context.ItemAttributes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ItemAttribute item)
        {
            context.ItemAttributes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ItemAttribute item)
        {
            context.ItemAttributes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ItemAttribute> GetByIdAsync(Guid id)
        {
            var item = await context.ItemAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ItemAttribute>> GetAllAsync()
        {
            return await context.ItemAttributes.ToListAsync();
        }
    }
}
