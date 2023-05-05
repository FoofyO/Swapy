using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly SwapyDbContext _context;

        public ChatRepository(SwapyDbContext context) => _context = context;

        public async Task CreateAsync(Chat item)
        {
            await _context.Chats.AddAsync(item);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Chat item)
        {
            _context.Chats.Update(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Chat item)
        {
            _context.Chats.Remove(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByIdAsync(Guid id) => await DeleteAsync(await GetByIdAsync(id));

        public async Task<Chat> GetByIdAsync(Guid id)
        {
            var item = await _context.Chats.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Chat>> GetAllAsync()
        {
            return await _context.Chats.ToListAsync();
        }
    }
}
