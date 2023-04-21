using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ModelsRepository : IModelsRepository
    {
        private readonly SwapyDbContext context;

        public ModelsRepository(SwapyDbContext context) => this.context = context;

        public void Create(Models item)
        {
            context.Models.Add(item);
            context.SaveChanges();
        }
        public void Update(Models item)
        {
            context.Models.Update(item);
            context.SaveChanges();
        }
        public void Delete(Models item)
        {
            context.Models.Remove(item);
            context.SaveChanges();
        }

        public Models GetById(Guid id)
        {
            var item = context.Models.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<Models> GetAll()
        {
            return context.Models.ToList();
        }
    }
}
