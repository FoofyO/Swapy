namespace Swapy.Common.Entities
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public Guid LikerId { get; set; }
        public User Liker { get; set; }

        public Like() { }

        public Like(Guid sellerId, Guid likerId) : this()
        {
            SellerId = sellerId;
            LikerId = likerId;
        }
    }
}
