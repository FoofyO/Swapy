using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesSizeRepository : IClothesSizeRepository
    {
        private readonly SwapyDbContext context;

        public ClothesSizeRepository(SwapyDbContext context) => this.context = context;

        public void Create(ClothesSize item)
        {
            context.ClothesSizes.Add(item);
            context.SaveChanges();
        }

        public void Delete(ClothesSize item)
        {
            context.ClothesSizes.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<ClothesSize> GetAll()
        {
            return context.ClothesSizes.ToList();
        }

        public ClothesSize GetById(Guid id)
        {
            var item = context.ClothesSizes.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(ClothesSize item)
        {
            context.ClothesSizes.Update(item);
            context.SaveChanges();
        }
    }
}
