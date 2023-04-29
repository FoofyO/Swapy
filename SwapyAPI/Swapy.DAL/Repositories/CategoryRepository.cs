using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    internal class CategoryRepository : ICategoryRepository
    {
        private readonly SwapyDbContext context;

        public CategoryRepository(SwapyDbContext context) => this.context = context;

        public void Create(Category item)
        {
            context.Categories.Add(item);
            context.SaveChanges();
        }

        public void Update(Category item)
        {
            context.Categories.Update(item);
            context.SaveChanges();
        }

        public void Delete(Category item)
        {
            context.Categories.Remove(item);
            context.SaveChanges();
        }

        public Category GetById(Guid id)
        {
            var item = context.Categories.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }
        
        public IEnumerable<Category> GetAll()
        {
            return context.Categories.ToList();
        }
    }
}
