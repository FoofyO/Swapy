using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoColorRepository : IAutoColorRepository
    {
        private readonly SwapyDbContext context;

        public AutoColorRepository(SwapyDbContext context) => this.context = context;

        public void Create(AutoColor item)
        {
            context.AutoColors.Add(item);
            context.SaveChanges();
        }
        public void Update(AutoColor item)
        {
            context.AutoColors.Update(item);
            context.SaveChanges();
        }
        public void Delete(AutoColor item)
        {
            context.AutoColors.Remove(item);
            context.SaveChanges();
        }

        public AutoColor GetById(Guid id)
        {
            var item = context.AutoColors.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AutoColor> GetAll()
        {
            return context.AutoColors.ToList();
        }
    }
}
