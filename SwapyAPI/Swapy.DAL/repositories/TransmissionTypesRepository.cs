using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swapy.DAL.Repositories
{
    public class TransmissionTypesRepository : ITransmissionTypesRepository
    {
        private readonly SwapyDbContext context;

        public TransmissionTypesRepository(SwapyDbContext context) => this.context = context;

        public void Create(TransmissionTypes item)
        {
            context.TransmissionTypes.Add(item);
            context.SaveChanges();
        }
        public void Update(TransmissionTypes item)
        {
            context.TransmissionTypes.Update(item);
            context.SaveChanges();
        }
        public void Delete(TransmissionTypes item)
        {
            context.TransmissionTypes.Remove(item);
            context.SaveChanges();
        }

        public TransmissionTypes GetById(Guid id)
        {
            var item = context.TransmissionTypes.Find(id);
            if (item == null) throw new ArgumentException("Not found!");
            return item;
        }

        public IEnumerable<TransmissionTypes> GetAll()
        {
            return context.TransmissionTypes.ToList();
        }
    }
}
