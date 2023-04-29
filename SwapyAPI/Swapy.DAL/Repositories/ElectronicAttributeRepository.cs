using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicAttributeRepository : IElectronicAttributeRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicAttributeRepository(SwapyDbContext context) => this.context = context;

        public void Create(ElectronicAttribute item)
        {
            context.ElectronicAttributes.Add(item);
            context.SaveChanges();
        }

        public void Update(ElectronicAttribute item)
        {
            context.ElectronicAttributes.Update(item);
            context.SaveChanges();
        }

        public void Delete(ElectronicAttribute item)
        {
            context.ElectronicAttributes.Remove(item);
            context.SaveChanges();
        }

        public ElectronicAttribute GetById(Guid id)
        {
            var item = context.ElectronicAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ElectronicAttribute> GetAll()
        {
            return context.ElectronicAttributes.ToList();
        }
    }
}
