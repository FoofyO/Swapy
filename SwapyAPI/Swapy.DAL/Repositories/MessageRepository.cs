using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SwapyDbContext context;

        public MessageRepository(SwapyDbContext context) => this.context = context;

        public void Create(Message item)
        {
            context.Messages.Add(item);
            context.SaveChanges();
        }

        public void Update(Message item)
        {
            context.Messages.Update(item);
            context.SaveChanges();
        }

        public void Delete(Message item)
        {
            context.Messages.Remove(item);
            context.SaveChanges();
        }

        public Message GetById(Guid id)
        {
            var item = context.Messages.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }       
        
        public IEnumerable<Message> GetAll()
        {
            return context.Messages.ToList();
        }
    }
}
