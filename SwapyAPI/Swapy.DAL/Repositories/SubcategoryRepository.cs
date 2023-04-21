using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    internal class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly SwapyDbContext context;

        public SubcategoryRepository(SwapyDbContext context) => this.context = context;

        public void Create(Subcategory item)
        {
            context.Subcategories.Add(item);
            context.SaveChanges();
        }

        public void Delete(Subcategory item)
        {
            context.Subcategories.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<Subcategory> GetAll()
        {
            return context.Subcategories.ToList();
        }

        public Subcategory GetById(Guid id)
        {
            var item = context.Subcategories.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(Subcategory item)
        {
            context.Subcategories.Update(item);
            context.SaveChanges();
        }
    }
}
