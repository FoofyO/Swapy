using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoAttributeRepository : IAutoAttributeRepository
    {
        private readonly SwapyDbContext context;
    
        public AutoAttributeRepository(SwapyDbContext context) => this.context = context;
        
        public void Create(AutoAttribute item)
        {
            context.AutoAttributes.Add(item);
            context.SaveChanges();
        }

        public void Update(AutoAttribute item)
        {
            context.AutoAttributes.Update(item);
            context.SaveChanges();
        }

        public void Delete(AutoAttribute item)
        {
            context.AutoAttributes.Remove(item);
            context.SaveChanges();
        }
    
        public AutoAttribute GetById(Guid id)
        {
            var item = context.AutoAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }
    
        public IEnumerable<AutoAttribute> GetAll()
        {
            return context.AutoAttributes.ToList();
        }
    }
}
