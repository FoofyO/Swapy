using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesBrandRepository : IClothesBrandRepository
    {
        private readonly SwapyDbContext context;

        public ClothesBrandRepository(SwapyDbContext context) => this.context = context;

        public void Create(ClothesBrand item)
        {
            context.ClothesBrands.Add(item);
            context.SaveChanges();
        }

        public void Delete(ClothesBrand item)
        {
            context.ClothesBrands.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<ClothesBrand> GetAll()
        {
            return context.ClothesBrands.ToList();
        }

        public ClothesBrand GetById(Guid id)
        {
            var item = context.ClothesBrands.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(ClothesBrand item)
        {
            context.ClothesBrands.Update(item);
            context.SaveChanges();
        }
    }
}
