using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicTypeRepository : IElectronicTypeRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(ElectronicType item)
        {
            context.ElectronicTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(ElectronicType item)
        {
            context.ElectronicTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ElectronicType item)
        {
            context.ElectronicTypes.Remove(item);
            context.SaveChanges();
        }

        public ElectronicType GetById(Guid id)
        {
            var item = context.ElectronicTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ElectronicType> GetAll()
        {
            return context.ElectronicTypes.ToList();
        }
    }
}
