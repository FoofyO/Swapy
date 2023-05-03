using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly SwapyDbContext context;

        public ChatRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Chat item)
        {
            await context.Chats.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chat item)
        {
            context.Chats.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Chat item)
        {
            context.Chats.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Chat> GetByIdAsync(Guid id)
        {
            var item = await context.Chats.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Chat>> GetAllAsync()
        {
            return await context.Chats.ToListAsync();
        }
    }
}
