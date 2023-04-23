using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ItemAttributeRepository : IItemAttributeRepository
    {
        private readonly SwapyDbContext context;

        public ItemAttributeRepository(SwapyDbContext context) => this.context = context;

        public void Create(ItemAttribute item)
        {
            context.ItemAttributes.Add(item);
            context.SaveChanges();
        }
        public void Update(ItemAttribute item)
        {
            context.ItemAttributes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ItemAttribute item)
        {
            context.ItemAttributes.Remove(item);
            context.SaveChanges();
        }

        public ItemAttribute GetById(Guid id)
        {
            var item = context.ItemAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ItemAttribute> GetAll()
        {
            return context.ItemAttributes.ToList();
        }
    }
}
