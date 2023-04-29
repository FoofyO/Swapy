using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ClothesAttributeRepository : IClothesAttributeRepository
    {
        private readonly SwapyDbContext context;

        public ClothesAttributeRepository(SwapyDbContext context) => this.context = context;

        public void Create(ClothesAttribute item)
        {
            context.ClothesAttributes.Add(item);
            context.SaveChanges();
        }

        public void Update(ClothesAttribute item)
        {
            context.ClothesAttributes.Update(item);
            context.SaveChanges();
        }

        public void Delete(ClothesAttribute item)
        {
            context.ClothesAttributes.Remove(item);
            context.SaveChanges();
        } 

        public ClothesAttribute GetById(Guid id)
        {
            var item = context.ClothesAttributes.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }
        
        public IEnumerable<ClothesAttribute> GetAll()
        {
            return context.ClothesAttributes.ToList();
        }
    }
}
 