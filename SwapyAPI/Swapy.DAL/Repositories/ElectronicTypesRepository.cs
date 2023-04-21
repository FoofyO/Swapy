using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ElectronicTypesRepository : IElectronicTypesRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(ElectronicTypes item)
        {
            context.ElectronicTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(ElectronicTypes item)
        {
            context.ElectronicTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ElectronicTypes item)
        {
            context.ElectronicTypes.Remove(item);
            context.SaveChanges();
        }

        public ElectronicTypes GetById(Guid id)
        {
            var item = context.ElectronicTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ElectronicTypes> GetAll()
        {
            return context.ElectronicTypes.ToList();
        }
    }
}
