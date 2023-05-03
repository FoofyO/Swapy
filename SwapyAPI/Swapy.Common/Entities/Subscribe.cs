namespace Swapy.Common.Entities
{
    public class Subscribe
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public Guid SubscriberId { get; set; }
        public User Subscriber { get; set; }

        public Subscribe() { }

        public Subscribe(Guid sellerId, Guid subscriberId) : this()
        {
            SellerId = sellerId;
            SubscriberId = subscriberId;
        }
    }
}
