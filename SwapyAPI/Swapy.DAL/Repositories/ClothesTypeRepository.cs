using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesSTypeRepository : IClothesSTypeRepository
    {
        private readonly SwapyDbContext context;

        public ClothesSTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(ClothesSType item)
        {
            context.ClothesTypes.Add(item);
            context.SaveChanges();
        }

        public void Delete(ClothesSType item)
        {
            context.ClothesTypes.Remove(item);
            context.SaveChanges();
        }
         
        public IEnumerable<ClothesSType> GetAll()
        {
            return context.ClothesTypes.ToList();
        }

        public ClothesSType GetById(Guid id)
        {
            var item = context.ClothesTypes.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(ClothesSType item)
        {
            context.ClothesTypes.Update(item);
            context.SaveChanges();
        }
    }
}
