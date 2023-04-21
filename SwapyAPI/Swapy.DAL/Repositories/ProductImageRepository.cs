using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ProductImageRepository : IProductImageRepository
    {
        private readonly SwapyDbContext context;

        public ProductImageRepository(SwapyDbContext context) => this.context = context;

        public void Create(ProductImage item)
        {
            context.ProductImages.Add(item);
            context.SaveChanges();
        }

        public void Delete(ProductImage item)
        {
            context.ProductImages.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<ProductImage> GetAll()
        {
            return context.ProductImages.ToList();
        }

        public ProductImage GetById(Guid id)
        {
            var item = context.ProductImages.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(ProductImage item)
        {
            context.ProductImages.Update(item);
            context.SaveChanges();
        }
    }
}
