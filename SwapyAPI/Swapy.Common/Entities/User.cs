using Microsoft.AspNetCore.Identity;
using Swapy.Common.Enums;

namespace Swapy.Common.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserType Type { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Logo { get; set; }
        public string RefreshTokenId { get; set; }
        public RefreshToken RefreshToken { get; set; }
        public string ShopAttributeId { get; set; }
        public ShopAttribute ShopAttribute { get; set; }

        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Chat> ChatsAsBuyer { get; set; } = new List<Chat>();
        public ICollection<Product> Products { get; set; } = new List<Product>();
        public ICollection<Message> SentMessages { get; set; } = new List<Message>();
        public ICollection<UserLike> LikesRecipient { get; set; } = new List<UserLike>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; } = new List<FavoriteProduct>();
        public ICollection<UserSubscription> SubscriptionsRecipient { get; set; } = new List<UserSubscription>();

        public User() { }
    }
}
