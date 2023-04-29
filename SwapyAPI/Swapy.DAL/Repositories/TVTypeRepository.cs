using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVTypesRepository : ITVTypeRepository
    {
        private readonly SwapyDbContext context;

        public TVTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(TVType item)
        {
            context.TVTypes.Add(item);
            context.SaveChanges();
        }

        public void Update(TVType item)
        {
            context.TVTypes.Update(item);
            context.SaveChanges();
        }

        public void Delete(TVType item)
        {
            context.TVTypes.Remove(item);
            context.SaveChanges();
        }

        public TVType GetById(Guid id)
        {
            var item = context.TVTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<TVType> GetAll()
        {
            return context.TVTypes.ToList();
        }
    }
}
