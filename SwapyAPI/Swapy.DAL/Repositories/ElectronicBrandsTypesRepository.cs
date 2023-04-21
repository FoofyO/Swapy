using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ElectronicBrandsTypesRepository : IElectronicBrandsTypesRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicBrandsTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(ElectronicBrandsTypes item)
        {
            context.ElectronicBrandsTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(ElectronicBrandsTypes item)
        {
            context.ElectronicBrandsTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(ElectronicBrandsTypes item)
        {
            context.ElectronicBrandsTypes.Remove(item);
            context.SaveChanges();
        }

        public ElectronicBrandsTypes GetById(Guid id)
        {
            var item = context.ElectronicBrandsTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ElectronicBrandsTypes> GetAll()
        {
            return context.ElectronicBrandsTypes.ToList();
        }
    }
}
