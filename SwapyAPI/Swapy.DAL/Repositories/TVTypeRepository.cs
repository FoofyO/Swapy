using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVTypesRepository : ITVTypesRepository
    {
        private readonly SwapyDbContext context;

        public TVTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(TVTypes item)
        {
            context.TVTypess.Add(item);
            context.SaveChanges();
        }
        public void Update(TVTypes item)
        {
            context.TVTypess.Update(item);
            context.SaveChanges();
        }
        public void Delete(TVTypes item)
        {
            context.TVTypess.Remove(item);
            context.SaveChanges();
        }

        public TVTypes GetById(Guid id)
        {
            var item = context.TVTypess.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<TVTypes> GetAll()
        {
            return context.TVTypess.ToList();
        }
    }
}
