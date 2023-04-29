using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class AutoBrandTypeRepository : IAutoBrandTypeRepository
    {
        private readonly SwapyDbContext context;

        public AutoBrandTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(AutoBrandType item)
        {
            context.AutoBrandsTypes.Add(item);
            context.SaveChanges();
        }

        public void Update(AutoBrandType item)
        {
            context.AutoBrandsTypes.Update(item);
            context.SaveChanges();
        }

        public void Delete(AutoBrandType item)
        {
            context.AutoBrandsTypes.Remove(item);
            context.SaveChanges();
        }

        public AutoBrandType GetById(Guid id)
        {
            var item = context.AutoBrandsTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<AutoBrandType> GetAll()
        {
            return context.AutoBrandsTypes.ToList();
        }
    }
}
