using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class LikeRepository : ILikeRepository
    {
        private readonly SwapyDbContext context;

        public LikeRepository(SwapyDbContext context) => this.context = context;

        public void Create(Like item)
        {
            context.Likes.Add(item);
            context.SaveChanges();
        }

        public void Delete(Like item)
        {
            context.Likes.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<Like> GetAll()
        {
            return context.Likes.ToList();
        }

        public Like GetById(Guid id)
        {
            var item = context.Likes.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(Like item)
        {
            context.Likes.Update(item);
            context.SaveChanges();
        }
    }
}
