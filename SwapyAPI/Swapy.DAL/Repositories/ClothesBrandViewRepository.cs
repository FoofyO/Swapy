using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesBrandViewRepository : IClothesBrandViewRepository
    {
        private readonly SwapyDbContext context;

        public ClothesBrandViewRepository(SwapyDbContext context) => this.context = context;

        public void Create(ClothesBrandView item)
        {
            context.ClothesBrandsViews.Add(item);
            context.SaveChanges();
        }

        public void Delete(ClothesBrandView item)
        {
            context.ClothesBrandsViews.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<ClothesBrandView> GetAll()
        {
            return context.ClothesBrandsViews.ToList();
        }

        public ClothesBrandView GetById(Guid id)
        {
            var item = context.ClothesBrandsViews.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(ClothesBrandView item)
        {
            context.ClothesBrandsViews.Update(item);
            context.SaveChanges();
        }
    }
}
