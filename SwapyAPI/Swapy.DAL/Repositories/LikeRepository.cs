using Microsoft.EntityFrameworkCore;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly SwapyDbContext context;

        public LikeRepository(SwapyDbContext context) => this.context = context;

        public async Task CreateAsync(Like item)
        {
            await context.Likes.AddAsync(item);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Like item)
        {
            context.Likes.Update(item);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Like item)
        {
            context.Likes.Remove(item);
            await context.SaveChangesAsync();
        }

        public async Task<Like> GetByIdAsync(Guid id)
        {
            var item = await context.Likes.FindAsync(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public async Task<IEnumerable<Like>> GetAllAsync()
        {
            return await context.Likes.ToListAsync();
        }
    }
}
