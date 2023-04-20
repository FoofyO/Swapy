using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandsRepository : IAutoBrandsRepository
    {
        private readonly SwapyDbContext context;

        public AutoBrandsRepository(SwapyDbContext context) => this.context = context;

        public void Create(AutoBrands item)
        {
            context.AutoBrands.Add(item);
            context.SaveChanges();
        }
        public void Update(AutoBrands item)
        {
            context.AutoBrands.Update(item);
            context.SaveChanges();
        }
        public void Delete(AutoBrands item)
        {
            context.AutoBrands.Remove(item);
            context.SaveChanges();
        }

        public AutoBrands GetById(Guid id)
        {
            var item = context.AutoBrands.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AutoBrands> GetAll()
        {
            return context.AutoBrands.ToList();
        }
    }
}

