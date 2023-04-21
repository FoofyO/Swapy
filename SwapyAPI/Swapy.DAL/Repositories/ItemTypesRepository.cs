using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ItemTypesRepository : IItemTypesRepository
    {
        private readonly SwapyDbContext context;

        public ItemTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(ItemTypes item)
        {
            context.ItemTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(ItemTypes item)
        {
            context.ItemTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ItemTypes item)
        {
            context.ItemTypes.Remove(item);
            context.SaveChanges();
        }

        public ItemTypes GetById(Guid id)
        {
            var item = context.ItemTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ItemTypes> GetAll()
        {
            return context.ItemTypes.ToList();
        }
    }
}
