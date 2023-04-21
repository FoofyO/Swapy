using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ItemAttributesRepository : IItemAttributesRepository
    {
        private readonly SwapyDbContext context;

        public ItemAttributesRepository(SwapyDbContext context) => this.context = context;

        public void Create(ItemAttributes item)
        {
            context.ItemAttributes.Add(item);
            context.SaveChanges();
        }
        public void Update(ItemAttributes item)
        {
            context.ItemAttributes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ItemAttributes item)
        {
            context.ItemAttributes.Remove(item);
            context.SaveChanges();
        }

        public ItemAttributes GetById(Guid id)
        {
            var item = context.ItemAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ItemAttributes> GetAll()
        {
            return context.ItemAttributes.ToList();
        }
    }
}
