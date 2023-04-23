namespace Swapy.DAL.Entities
{
    public class Subscribe
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public Guid SubscriberId { get; set; }
        public User Subscriber { get; set; }

        public Subscribe() { }

<<<<<<< HEAD
        public Subscribe(Guid sellerId, Guid subscriberId) : this()
=======
        public Subscribe(Guid sellerId, Guid subscriberId)
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        {
            SellerId = sellerId;
            SubscriberId = subscriberId;
        }
    }
}
