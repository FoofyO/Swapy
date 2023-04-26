using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    internal class ClothesViewRepository : IClothesViewRepository
    {
        private readonly SwapyDbContext context;

        public ClothesViewRepository(SwapyDbContext context) => this.context = context;

        public void Create(ClothesView item)
        {
            context.ClothesViews.Add(item);
            context.SaveChanges();
        }

        public void Delete(ClothesView item)
        {
            context.ClothesViews.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<ClothesView> GetAll()
        {
            return context.ClothesViews.ToList();
        }

        public ClothesView GetById(Guid id)
        {
            var item = context.ClothesViews.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(ClothesView item)
        {
            context.ClothesViews.Update(item);
            context.SaveChanges();
        }
    }
}
