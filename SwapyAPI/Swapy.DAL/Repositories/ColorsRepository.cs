using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ColorsRepository : IColorsRepository
    {
        private readonly SwapyDbContext context;

        public ColorsRepository(SwapyDbContext context) => this.context = context;

        public void Create(Colors item)
        {
            context.Colors.Add(item);
            context.SaveChanges();
        }
        public void Update(Colors item)
        {
            context.Colors.Update(item);
            context.SaveChanges();
        }
        public void Delete(Colors item)
        {
            context.Colors.Remove(item);
            context.SaveChanges();
        }

        public Colors GetById(Guid id)
        {
            var item = context.Colors.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<Colors> GetAll()
        {
            return context.Colors.ToList();
        }
    }
}

