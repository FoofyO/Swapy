using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandsTypesRepository : IAutoBrandsTypesRepository
    {
        private readonly SwapyDbContext context;

        public AutoBrandsTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(AutoBrandsTypes item)
        {
            context.AutoBrandsTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(AutoBrandsTypes item)
        {
            context.AutoBrandsTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(AutoBrandsTypes item)
        {
            context.AutoBrandsTypes.Remove(item);
            context.SaveChanges();
        }

        public AutoBrandsTypes GetById(Guid id)
        {
            var item = context.AutoBrandsTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AutoBrandsTypes> GetAll()
        {
            return context.AutoBrandsTypes.ToList();
        }
    }
}
