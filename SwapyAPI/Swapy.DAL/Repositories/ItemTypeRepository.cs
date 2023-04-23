using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ItemTypeRepository : IItemTypeRepository
    {
        private readonly SwapyDbContext context;

        public ItemTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(ItemType item)
        {
            context.ItemTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(ItemType item)
        {
            context.ItemTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ItemType item)
        {
            context.ItemTypes.Remove(item);
            context.SaveChanges();
        }

        public ItemType GetById(Guid id)
        {
            var item = context.ItemTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ItemType> GetAll()
        {
            return context.ItemTypes.ToList();
        }
    }
}
