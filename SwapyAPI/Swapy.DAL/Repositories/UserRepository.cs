using Swapy.DAL.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SwapyDbContext context;

        public UserRepository(SwapyDbContext context) => this.context = context;

        public void Create(User item)
        {
            context.Users.Add(item);
            context.SaveChanges();
        }

        public void Delete(User item)
        {
            context.Users.Remove(item);
            context.SaveChanges();
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users.ToList();
        }

        public User GetById(Guid id)
        {
            var item = context.Users.Find(id);
            if (item == null) throw new Exception("Not found!");
            return item;
        }

        public void Update(User item)
        {
            context.Users.Update(item);
            context.SaveChanges();
        }
    }
}
