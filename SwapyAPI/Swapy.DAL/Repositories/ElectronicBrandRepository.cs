using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ElectronicBrandRepository : IElectronicBrandRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicBrandRepository(SwapyDbContext context) => this.context = context;

        public void Create(ElectronicBrand item)
        {
            context.ElectronicBrands.Add(item);
            context.SaveChanges();
        }
        public void Update(ElectronicBrand item)
        {
            context.ElectronicBrands.Update(item);
            context.SaveChanges();
        }
        public void Delete(ElectronicBrand item)
        {
            context.ElectronicBrands.Remove(item);
            context.SaveChanges();
        }

        public ElectronicBrand GetById(Guid id)
        {
            var item = context.ElectronicBrands.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ElectronicBrand> GetAll()
        {
            return context.ElectronicBrands.ToList();
        }
    }
}
