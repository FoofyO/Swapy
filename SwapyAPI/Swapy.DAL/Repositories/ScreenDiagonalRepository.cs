using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ScreenDiagonalRepository : IScreenDiagonalRepository
    {
        private readonly SwapyDbContext context;

        public ScreenDiagonalRepository(SwapyDbContext context) => this.context = context;

        public void Create(ScreenDiagonal item)
        {
            context.ScreenDiagonals.Add(item);
            context.SaveChanges();
        }
        public void Update(ScreenDiagonal item)
        {
            context.ScreenDiagonals.Update(item);
            context.SaveChanges();
        }
        public void Delete(ScreenDiagonal item)
        {
            context.ScreenDiagonals.Remove(item);
            context.SaveChanges();
        }

        public ScreenDiagonal GetById(Guid id)
        {
            var item = context.ScreenDiagonals.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ScreenDiagonal> GetAll()
        {
            return context.ScreenDiagonals.ToList();
        }
    }
}
