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
            context.Genders.Add(item);
            context.SaveChanges();
        }

        public void Update(Gender item)
        {
            context.Genders.Update(item);
            context.SaveChanges();
        }

        public void Delete(Gender item)
        {
            context.Genders.Remove(item);
            context.SaveChanges();
        }
        
        public Gender GetById(Guid id)
        {
            var item = context.Genders.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public IEnumerable<Gender> GetAll()
        {
            return context.Genders.ToList();
        }
    }
}
