using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ModelColorRepository : IModelColorRepository
    {
        private readonly SwapyDbContext context;

        public ModelColorRepository(SwapyDbContext context) => this.context = context;

        public void Create(ModelColor item)
        {
            context.ModelsColors.Add(item);
            context.SaveChanges();
        }
        public void Update(ModelColor item)
        {
            context.ModelsColors.Update(item);
            context.SaveChanges();
        }
        public void Delete(ModelColor item)
        {
            context.ModelsColors.Remove(item);
            context.SaveChanges();
        }

        public ModelColor GetById(Guid id)
        {
            var item = context.ModelsColors.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ModelColor> GetAll()
        {
            return context.ModelsColors.ToList();
        }
    }
}
