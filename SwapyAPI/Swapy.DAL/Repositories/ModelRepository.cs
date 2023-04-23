using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ModelRepository : IModelRepository
    {
        private readonly SwapyDbContext context;

        public ModelRepository(SwapyDbContext context) => this.context = context;

        public void Create(Model item)
        {
            context.Models.Add(item);
            context.SaveChanges();
        }
        public void Update(Model item)
        {
            context.Models.Update(item);
            context.SaveChanges();
        }
        public void Delete(Model item)
        {
            context.Models.Remove(item);
            context.SaveChanges();
        }

        public Model GetById(Guid id)
        {
            var item = context.Models.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<Model> GetAll()
        {
            return context.Models.ToList();
        }
    }
}
