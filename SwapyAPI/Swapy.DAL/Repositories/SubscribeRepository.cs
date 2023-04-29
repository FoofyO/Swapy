using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class SubscribeRepository : ISubscribeRepository
    {
        private readonly SwapyDbContext context;

        public SubscribeRepository(SwapyDbContext context) => this.context = context;

        public void Create(Subscribe item)
        {
            context.Subscribes.Add(item);
            context.SaveChanges();
        }

        public void Update(Subscribe item)
        {
            context.Subscribes.Update(item);
            context.SaveChanges();
        }
        
        public void Delete(Subscribe item)
        {
            context.Subscribes.Remove(item);
            context.SaveChanges();
        }

        public Subscribe GetById(Guid id)
        {
            var item = context.Subscribes.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public IEnumerable<Subscribe> GetAll()
        {
            return context.Subscribes.ToList();
        }
    }
}
