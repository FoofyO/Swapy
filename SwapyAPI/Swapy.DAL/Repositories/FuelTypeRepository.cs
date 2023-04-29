using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class FuelTypeRepository : IFuelTypeRepository
    {
        private readonly SwapyDbContext context;

        public FuelTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(FuelType item)
        {
            context.FuelTypes.Add(item);
            context.SaveChanges();
        }

        public void Update(FuelType item)
        {
            context.FuelTypes.Update(item);
            context.SaveChanges();
        }

        public void Delete(FuelType item)
        {
            context.FuelTypes.Remove(item);
            context.SaveChanges();
        }

        public FuelType GetById(Guid id)
        {
            var item = context.FuelTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<FuelType> GetAll()
        {
            return context.FuelTypes.ToList();
        }
    }
}
