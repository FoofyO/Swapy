using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{ 
    public class ScreenResolutionRepository : IScreenResolutionRepository
    {
        private readonly SwapyDbContext context;

        public ScreenResolutionRepository(SwapyDbContext context) => this.context = context;

        public void Create(ScreenResolution item)
        {
            context.ScreenResolutions.Add(item);
            context.SaveChanges();
        }

        public void Update(ScreenResolution item)
        {
            context.ScreenResolutions.Update(item);
            context.SaveChanges();
        }

        public void Delete(ScreenResolution item)
        {
            context.ScreenResolutions.Remove(item);
            context.SaveChanges(); 
        }

        public ScreenResolution GetById(Guid id)
        {
            var item = context.ScreenResolutions.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ScreenResolution> GetAll()
        {
            return context.ScreenResolutions.ToList();
        }
    }
}
