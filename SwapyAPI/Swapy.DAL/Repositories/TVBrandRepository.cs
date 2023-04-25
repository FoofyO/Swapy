using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TVBrandRepository : ITVBrandRepository
    {
        private readonly SwapyDbContext context;

        public TVBrandRepository(SwapyDbContext context) => this.context = context;

        public void Create(TVBrand item)
        {
            context.TVBrands.Add(item);
            context.SaveChanges();
        }
        public void Update(TVBrand item)
        {
            context.TVBrands.Update(item);
            context.SaveChanges();
        }
        public void Delete(TVBrand item)
        {
            context.TVBrands.Remove(item);
            context.SaveChanges();
        }

        public TVBrand GetById(Guid id)
        {
            var item = context.TVBrands.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<TVBrand> GetAll()
        {
            return context.TVBrands.ToList();
        }
    }
}
