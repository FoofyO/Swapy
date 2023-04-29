using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly SwapyDbContext context;

        public ChatRepository(SwapyDbContext context) => this.context = context;

        public void Create(Chat item)
        {
            context.Chats.Add(item);
            context.SaveChanges();
        }

        public void Update(Chat item)
        {
            context.Chats.Update(item);
            context.SaveChanges();
        }

        public void Delete(Chat item)
        {
            context.Chats.Remove(item);
            context.SaveChanges();
        }

        public Chat GetById(Guid id)
        {
            var item = context.Chats.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }
        
        public IEnumerable<Chat> GetAll()
        {
            return context.Chats.ToList();
        }
    }
}
