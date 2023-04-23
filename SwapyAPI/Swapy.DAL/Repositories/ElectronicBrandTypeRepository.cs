using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicBrandTypeRepository : IElectronicBrandTypeRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicBrandTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(ElectronicBrandType item)
        {
            context.ElectronicBrandsTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(ElectronicBrandType item)
        {
            context.ElectronicBrandsTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ElectronicBrandType item)
        {
            context.ElectronicBrandsTypes.Remove(item);
            context.SaveChanges();
        }

        public ElectronicBrandType GetById(Guid id)
        {
            var item = context.ElectronicBrandsTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ElectronicBrandType> GetAll()
        {
            return context.ElectronicBrandsTypes.ToList();
        }
    }
}
