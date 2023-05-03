using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicAttributeRepository : IElectronicAttributeRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicAttributeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(ElectronicAttribute item)
        {
            await context.ElectronicAttributes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ElectronicAttribute item)
        {
            context.ElectronicAttributes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ElectronicAttribute item)
        {
            context.ElectronicAttributes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<ElectronicAttribute> GetByIdAsync(Guid id)
        {
            var item = await context.ElectronicAttributes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<ElectronicAttribute>> GetAllAsync()
        {
            return await context.ElectronicAttributes.ToListAsync();
        }
    }
}
