using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class AutoColorsRepository : IAutoColorsRepository
    {
        private readonly SwapyDbContext context;

        public AutoColorsRepository(SwapyDbContext context) => this.context = context;

        public void Create(AutoColors item)
        {
            context.AutoColors.Add(item);
            context.SaveChanges();
        }
        public void Update(AutoColors item)
        {
            context.AutoColors.Update(item);
            context.SaveChanges();
        }
        public void Delete(AutoColors item)
        {
            context.AutoColors.Remove(item);
            context.SaveChanges();
        }

        public AutoColors GetById(Guid id)
        {
            var item = context.AutoColors.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AutoColors> GetAll()
        {
            return context.AutoColors.ToList();
        }
    }
}
