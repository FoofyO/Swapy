using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly SwapyDbContext context;

        public CurrencyRepository(SwapyDbContext context) => this.context = context;

        public void Create(Currency item)
        {
            context.Currencies.Add(item);
            context.SaveChanges();
        }

        public void Delete(Currency item)
        {
            context.Currencies.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<Currency> GetAll()
        {
            return context.Currencies.ToList();
        }

        public Currency GetById(Guid id)
        {
            var item = context.Currencies.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(Currency item)
        {
            context.Currencies.Update(item);
            context.SaveChanges();
        }
    }
}
