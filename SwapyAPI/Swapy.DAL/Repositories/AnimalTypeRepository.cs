using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalTypeRepository : IAnimalTypeRepository
    {
        private readonly SwapyDbContext context;

        public AnimalTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(AnimalType item)
        {
            context.AnimalTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(AnimalType item) 
        {
            context.AnimalTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(AnimalType item)
        {
            context.AnimalTypes.Remove(item);
            context.SaveChanges();
        }

        public AnimalType GetById(Guid id)
        {
            var item = context.AnimalTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AnimalType> GetAll()
        {
            return context.AnimalTypes.ToList();
        }
    }
}
