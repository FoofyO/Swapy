using Microsoft.AspNetCore.Identity;

namespace Swapy.DAL.Entities
{
    public class User : IdentityUser<Guid>
    {
        public string FullName { get; set; }
        public string Logo { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Like> LikesAsLiker { get; set; }
        public ICollection<Like> LikesAsSeller { get; set; }
        public ICollection<Chat> ChatsAsBuyer { get; set; }
        public ICollection<Chat> ChatsAsSeller { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Subscribe> SubscribesAsSeller { get; set; }
        public ICollection<Subscribe> SubscribesAsSubscriber { get; set; }

        public User()
        {
            LikesAsLiker = new List<Like>();
            LikesAsSeller = new List<Like>();
            ChatsAsBuyer = new List<Chat>();
            ChatsAsSeller = new List<Chat>();
            SentMessages = new List<Message>();
            SubscribesAsSeller = new List<Subscribe>();
            SubscribesAsSubscriber = new List<Subscribe>();
        }

<<<<<<< HEAD
        public User(string fullName, string logo) : this()
=======
        public User(string fullName, string logo)
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        {
            FullName = fullName;
            Logo = logo;
        }
    }
}
