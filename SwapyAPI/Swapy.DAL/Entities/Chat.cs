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
       
        public Chat(Guid sellerId, Guid buyerId) : this()
        {
            SellerId = sellerId;
            BuyerId = buyerId;
        }
    }
}
