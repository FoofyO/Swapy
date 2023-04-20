using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class FuelTypesRepository : IFuelTypesRepository
    {
        private readonly SwapyDbContext context;

        public FuelTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(FuelTypes item)
        {
            context.FuelTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(FuelTypes item)
        {
            context.FuelTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(FuelTypes item)
        {
            context.FuelTypes.Remove(item);
            context.SaveChanges();
        }

        public FuelTypes GetById(Guid id)
        {
            var item = context.FuelTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<FuelTypes> GetAll()
        {
            return context.FuelTypes.ToList();
        }
    }
}
