using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandRepository : IAutoBrandRepository
    {
        private readonly SwapyDbContext context;

        public AutoBrandRepository(SwapyDbContext context) => this.context = context;

        public void Create(AutoBrand item)
        {
            context.AutoBrands.Add(item);
            context.SaveChanges();
        }
        public void Update(AutoBrand item)
        {
            context.AutoBrands.Update(item);
            context.SaveChanges();
        }
        public void Delete(AutoBrand item)
        {
            context.AutoBrands.Remove(item);
            context.SaveChanges();
        }

        public AutoBrand GetById(Guid id)
        {
            var item = context.AutoBrands.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AutoBrand> GetAll()
        {
            return context.AutoBrands.ToList();
        }
    }
}

