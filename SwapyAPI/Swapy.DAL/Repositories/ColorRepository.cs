using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ColorRepository : IColorRepository
    {
        private readonly SwapyDbContext context;

        public ColorRepository(SwapyDbContext context) => this.context = context;

        public void Create(Color item)
        {
            context.Colors.Add(item);
            context.SaveChanges();
        }

        public void Update(Color item)
        {
            context.Colors.Update(item);
            context.SaveChanges();
        }

        public void Delete(Color item)
        {
            context.Colors.Remove(item);
            context.SaveChanges();
        }

        public Color GetById(Guid id)
        {
            var item = context.Colors.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<Color> GetAll()
        {
            return context.Colors.ToList();
        }
    }
}

