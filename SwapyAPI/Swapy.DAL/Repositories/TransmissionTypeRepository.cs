using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TransmissionTypeRepository : ITransmissionTypeRepository
    {
        private readonly SwapyDbContext context;

        public TransmissionTypeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(TransmissionType item)
        {
            await context.TransmissionTypes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TransmissionType item)
        {
            context.TransmissionTypes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TransmissionType item)
        {
            context.TransmissionTypes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<TransmissionType> GetByIdAsync(Guid id)
        {
            var item = await context.TransmissionTypes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<TransmissionType>> GetAllAsync()
        {
            return await context.TransmissionTypes.ToListAsync();
        }
    }
}
