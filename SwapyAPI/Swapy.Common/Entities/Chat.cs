namespace Swapy.Common.Entities
{
    public class Chat
    {
        public Guid Id { get; set; }
        public Guid BuyerId { get; set; }
        public User Buyer { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public ICollection<Message> Messages { get; set; }

        public Chat() => Messages = new List<Message>();
       
        public Chat(Guid productId, Guid buyerId) : this()
        {
            ProductId = productId;
            BuyerId = buyerId;
        }
    }
}
