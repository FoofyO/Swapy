using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    internal class ClothesSeasonRepository : IClothesSeasonRepository
    {
        private readonly SwapyDbContext context;

        public ClothesSeasonRepository(SwapyDbContext context) => this.context = context;

        public void Create(ClothesSeason item)
        {
            context.ClothesSeasons.Add(item);
            context.SaveChanges();
        }
         
        public void Delete(ClothesSeason item)
        {
            context.ClothesSeasons.Remove(item);
            context.SaveChanges();
        }
         
        public IEnumerable<ClothesSeason> GetAll()
        {
            return context.ClothesSeasons.ToList();
        }

        public ClothesSeason GetById(Guid id)
        {
            var item = context.ClothesSeasons.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(ClothesSeason item)
        {
            context.ClothesSeasons.Update(item);
            context.SaveChanges();
        }
    }
}
