namespace Swapy.DAL.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public Guid BuyerId { get; set; }
        public User Buyer { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Chat() => Messages = new List<Message>();

<<<<<<< HEAD
        public Chat(Guid sellerId, Guid buyerId) : this()
=======
        public Chat(Guid sellerId, Guid buyerId)
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        {
            SellerId = sellerId;
            BuyerId = buyerId;
        }
    }
}
