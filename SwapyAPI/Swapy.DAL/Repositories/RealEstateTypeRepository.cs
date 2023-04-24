using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class RealEstateTypeRepository : IRealEstateTypeRepository
    {
        private readonly SwapyDbContext context;

        public RealEstateTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(RealEstateType item)
        {
            context.RealEstateTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(RealEstateType item)
        {
            context.RealEstateTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(RealEstateType item)
        {
            context.RealEstateTypes.Remove(item);
            context.SaveChanges();
        }

        public RealEstateType GetById(Guid id)
        {
            var item = context.RealEstateTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<RealEstateType> GetAll()
        {
            return context.RealEstateTypes.ToList();
        }
    }
}
