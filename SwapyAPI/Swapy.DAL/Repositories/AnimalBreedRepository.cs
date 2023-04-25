using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalBreedRepository : IAnimalBreedRepository
    {
        private readonly SwapyDbContext context;

        public AnimalBreedRepository(SwapyDbContext context) => this.context = context;

        public void Create(AnimalBreed item)
        {
            context.AnimalBreeds.Add(item);
            context.SaveChanges();
        }
        public void Update(AnimalBreed item)
        {
            context.AnimalBreeds.Update(item);
            context.SaveChanges();
        }
        public void Delete(AnimalBreed item)
        {
            context.AnimalBreeds.Remove(item);
            context.SaveChanges();
        }

        public AnimalBreed GetById(Guid id)
        {
            var item = context.AnimalBreeds.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AnimalBreed> GetAll()
        {
            return context.AnimalBreeds.ToList();
        }
    }
}
