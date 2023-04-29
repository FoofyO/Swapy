using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    internal class GenderRepository : IGenderRepository
    {
        private readonly SwapyDbContext context;

        public GenderRepository(SwapyDbContext context) => this.context = context;

        public void Create(Gender item)
        {
            context.Gender.Add(item);
            context.SaveChanges();
        }

        public void Delete(Gender item)
        {
            context.Gender.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<Gender> GetAll()
        {
            return context.Gender.ToList();
        }

        public Gender GetById(Guid id)
        {
            var item = context.Gender.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(Gender item)
        {
            context.Gender.Update(item);
            context.SaveChanges();
        }
    }
}
