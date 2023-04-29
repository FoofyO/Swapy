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
            context.ClothesBrandViews.Add(item);
            context.SaveChanges();
        }

        public void Update(ClothesBrandView item)
        {
            context.ClothesBrandViews.Update(item);
            context.SaveChanges();
        }

        public void Delete(ClothesBrandView item)
        {
            context.ClothesBrandViews.Remove(item);
            context.SaveChanges();
        }

        public ClothesBrandView GetById(Guid id)
        {
            var item = context.ClothesBrandViews.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }
        
        public IEnumerable<ClothesBrandView> GetAll()
        {
            return context.ClothesBrandViews.ToList();
        }
    }
}
