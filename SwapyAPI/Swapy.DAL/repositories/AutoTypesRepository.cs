using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class AutoTypesRepository : IAutoTypesRepository
    {
        private readonly SwapyDbContext context;

        public AutoTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(AutoTypes item)
        {
            context.AutoTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(AutoTypes item)
        {
            context.AutoTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(AutoTypes item)
        {
            context.AutoTypes.Remove(item);
            context.SaveChanges();
        }

        public AutoTypes GetById(Guid id)
        {
            var item = context.AutoTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AutoTypes> GetAll()
        {
            return context.AutoTypes.ToList();
        }
    }
}
