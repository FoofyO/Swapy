using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace Swapy.DAL.Repositories
{
    public class AutoAttributesRepository : IAutoAttributesRepository
    {
        private readonly SwapyDbContext context;
    
        public AutoAttributesRepository(SwapyDbContext context) => this.context = context;
        
        public void Create(AutoAttributes item)
        {
            context.AutoAttributes.Add(item);
            context.SaveChanges();
        }
        public void Update(AutoAttributes item)
        {
            context.AutoAttributes.Update(item);
            context.SaveChanges();
        }
        public void Delete(AutoAttributes item)
        {
            context.AutoAttributes.Remove(item);
            context.SaveChanges();
        }
    
        public AutoAttributes GetById(Guid id)
        {
            var item = context.AutoAttributes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }
    
        public IEnumerable<AutoAttributes> GetAll()
        {
            return context.AutoAttributes.ToList();
        }
    }
}
