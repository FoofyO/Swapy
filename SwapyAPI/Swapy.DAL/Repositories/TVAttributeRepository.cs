using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVAttributeRepository : ITVAttributeRepository
    {
        private readonly SwapyDbContext context;
    
        public TVAttributeRepository(SwapyDbContext context) => this.context = context;
        
        public void Create(TVAttribute item)
        {
            context.TVAttributes.Add(item); 
            context.SaveChanges();
        }

        public void Update(TVAttribute item)
        {
            context.TVAttributes.Update(item);
            context.SaveChanges();
        }

        public void Delete(TVAttribute item)
        {
            context.TVAttributes.Remove(item);
            context.SaveChanges();
        }
    
        public TVAttribute GetById(Guid id)
        {
            var item = context.TVAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }
    
        public IEnumerable<TVAttribute> GetAll()
        {
            return context.TVAttributes.ToList();
        }
    }
} 
