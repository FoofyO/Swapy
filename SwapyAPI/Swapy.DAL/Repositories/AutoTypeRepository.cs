using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoTypeRepository : IAutoTypeRepository
    {
        private readonly SwapyDbContext context;

        public AutoTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(AutoType item)
        {
            context.AutoTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(AutoType item)
        {
            context.AutoTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(AutoType item)
        {
            context.AutoTypes.Remove(item);
            context.SaveChanges();
        }

        public AutoType GetById(Guid id)
        {
            var item = context.AutoTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AutoType> GetAll()
        {
            return context.AutoTypes.ToList();
        }
    }
}
