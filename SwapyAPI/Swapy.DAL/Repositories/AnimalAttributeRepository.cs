using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AnimalAttributeRepository : IAnimalAttributeRepository
    {
        private readonly SwapyDbContext context;
    
        public AnimalAttributeRepository(SwapyDbContext context) => this.context = context;
        
        public void Create(AnimalAttribute item)
        {
            context.AnimalAttributes.Add(item); 
            context.SaveChanges();
        }

        public void Update(AnimalAttribute item)
        {
            context.AnimalAttributes.Update(item);
            context.SaveChanges();
        }

        public void Delete(AnimalAttribute item)
        {
            context.AnimalAttributes.Remove(item);
            context.SaveChanges();
        }
    
        public AnimalAttribute GetById(Guid id)
        {
            var item = context.AnimalAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }
    
        public IEnumerable<AnimalAttribute> GetAll()
        {
            return context.AnimalAttributes.ToList();
        }
    }
}
