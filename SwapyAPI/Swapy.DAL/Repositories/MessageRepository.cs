using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SwapyDbContext context;

        public MessageRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Message item)
        {
            await context.Messages.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Message item)
        {
            context.Messages.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Message item)
        {
            context.Messages.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Message> GetByIdAsync(Guid id)
        {
            var item = await context.Messages.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Message>> GetAllAsync()
        {
            return await context.Messages.ToListAsync();
        }
    }
}
