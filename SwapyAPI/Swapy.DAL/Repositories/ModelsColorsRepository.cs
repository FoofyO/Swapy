using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class ModelsColorsRepository : IModelsColorsRepository
    {
        private readonly SwapyDbContext context;

        public ModelsColorsRepository(SwapyDbContext context) => this.context = context;

        public void Create(ModelsColors item)
        {
            context.ModelsColors.Add(item);
            context.SaveChanges();
        }
        public void Update(ModelsColors item)
        {
            context.ModelsColors.Update(item);
            context.SaveChanges();
        }
        public void Delete(ModelsColors item)
        {
            context.ModelsColors.Remove(item);
            context.SaveChanges();
        }

        public ModelsColors GetById(Guid id)
        {
            var item = context.ModelsColors.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<ModelsColors> GetAll()
        {
            return context.ModelsColors.ToList();
        }
    }
}
