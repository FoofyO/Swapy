using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ElectronicAttributesRepository : IElectronicAttributesRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicAttributesRepository(SwapyDbContext context) => this.context = context;

        public void Create(ElectronicAttributes item)
        {
            context.ElectronicAttributes.Add(item);
            context.SaveChanges();
        }
        public void Update(ElectronicAttributes item)
        {
            context.ElectronicAttributes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ElectronicAttributes item)
        {
            context.ElectronicAttributes.Remove(item);
            context.SaveChanges();
        }

        public ElectronicAttributes GetById(Guid id)
        {
            var item = context.ElectronicAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ElectronicAttributes> GetAll()
        {
            return context.ElectronicAttributes.ToList();
        }
    }
}
