using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class CityRepository : ICityRepository
    {
        private readonly SwapyDbContext context;

        public CityRepository(SwapyDbContext context) => this.context = context;

        public void Create(City item)
        {
            context.Cities.Add(item);
            context.SaveChanges();
        }

        public void Update(City item)
        {
            context.Cities.Update(item);
            context.SaveChanges();
        }

        public void Delete(City item)
        {
            context.Cities.Remove(item);
            context.SaveChanges();
        }

        public City GetById(Guid id)
        {
            var item = context.Cities.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }
        
        public IEnumerable<City> GetAll()
        {
            return context.Cities.ToList();
        }
    }
}
