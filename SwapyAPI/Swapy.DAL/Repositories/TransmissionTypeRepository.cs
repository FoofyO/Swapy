using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class TransmissionTypeRepository : ITransmissionTypeRepository
    {
        private readonly SwapyDbContext context;

        public TransmissionTypeRepository(SwapyDbContext context) => this.context = context;

        public void Create(TransmissionType item)
        {
            context.TransmissionTypes.Add(item);
            context.SaveChanges();
        }

        public void Update(TransmissionType item)
        {
            context.TransmissionTypes.Update(item);
            context.SaveChanges();
        }

        public void Delete(TransmissionType item)
        {
            context.TransmissionTypes.Remove(item);
            context.SaveChanges();
        }

        public TransmissionType GetById(Guid id)
        {
            var item = context.TransmissionTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<TransmissionType> GetAll()
        {
            return context.TransmissionTypes.ToList();
        }
    }
}
