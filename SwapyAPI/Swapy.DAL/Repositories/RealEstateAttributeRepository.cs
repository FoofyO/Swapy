using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class RealEstateAttributeRepository : IRealEstateAttributeRepository
    {
        private readonly SwapyDbContext context;
    
        public RealEstateAttributeRepository(SwapyDbContext context) => this.context = context;
        
        public void Create(RealEstateAttribute item)
        {
            context.RealEstateAttributes.Add(item);
            context.SaveChanges();
        }
        public void Update(RealEstateAttribute item)
        {
            context.RealEstateAttributes.Update(item);
            context.SaveChanges();
        }
        public void Delete(RealEstateAttribute item)
        {
            context.RealEstateAttributes.Remove(item);
            context.SaveChanges();
        }
    
        public RealEstateAttribute GetById(Guid id)
        {
            var item = context.RealEstateAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }
    
        public IEnumerable<RealEstateAttribute> GetAll()
        {
            return context.RealEstateAttributes.ToList();
        }
    }
}
