using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ElectronicBrandsRepository : IElectronicBrandsRepository
    {
        private readonly SwapyDbContext context;

        public ElectronicBrandsRepository(SwapyDbContext context) => this.context = context;

        public void Create(ElectronicBrands item)
        {
            context.ElectronicBrands.Add(item);
            context.SaveChanges();
        }
        public void Update(ElectronicBrands item)
        {
            context.ElectronicBrands.Update(item);
            context.SaveChanges();
        }
        public void Delete(ElectronicBrands item)
        {
            context.ElectronicBrands.Remove(item);
            context.SaveChanges();
        }

        public ElectronicBrands GetById(Guid id)
        {
            var item = context.ElectronicBrands.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ElectronicBrands> GetAll()
        {
            return context.ElectronicBrands.ToList();
        }
    }
}
