﻿using Microsoft.AspNetCore.Identity;

namespace Swapy.Common.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
        public string Logo { get; set; }
        public DateTime RegistrationDate { get; set; }

        public ICollection<Product> Products { get; set; }
        public ICollection<Like> LikesAsLiker { get; set; }
        public ICollection<Like> LikesAsSeller { get; set; }
        public ICollection<Chat> ChatsAsBuyer { get; set; }
        public ICollection<Message> SentMessages { get; set; }
        public ICollection<Subscribe> SubscribesAsSeller { get; set; }
        public ICollection<FavoriteProduct> FavoriteProducts { get; set; }
        public ICollection<Subscribe> SubscribesAsSubscriber { get; set; }

        public User()
        {
            Products = new List<Product>();
            LikesAsLiker = new List<Like>();
            LikesAsSeller = new List<Like>();
            ChatsAsBuyer = new List<Chat>();
            SentMessages = new List<Message>();
            SubscribesAsSeller = new List<Subscribe>();
            FavoriteProducts = new List<FavoriteProduct>();
            SubscribesAsSubscriber = new List<Subscribe>();
        }

        public User(string fullName, string email, string phone, string logo) : this()
        {
            Logo = logo;
            Email = email;
            FullName = fullName;
            PhoneNumber = phone;
        }
    }
}
