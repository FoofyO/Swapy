using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly SwapyDbContext context;

        public ProductRepository(SwapyDbContext context) => this.context = context;

        public void Create(Product item)
        {
            context.Products.Add(item);
            context.SaveChanges();
        }

        public void Update(Product item)
        {
            context.Products.Update(item);
            context.SaveChanges();
        }

        public void Delete(Product item)
        {
            context.Products.Remove(item);
            context.SaveChanges();
        }

        public Product GetById(Guid id)
        {
            var item = context.Products.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }
        
        public IEnumerable<Product> GetAll()
        {
            return context.Products.ToList();
        }
    }
}
