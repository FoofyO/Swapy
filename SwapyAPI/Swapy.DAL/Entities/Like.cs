namespace Swapy.DAL.Entities
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid SellerId { get; set; }
        public User Seller { get; set; }
        public Guid LikerId { get; set; }
        public User Liker { get; set; }

        public Like() { }

<<<<<<< HEAD
        public Like(Guid sellerId, Guid likerId) : this()
=======
        public Like(Guid sellerId, Guid likerId)
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        {
            SellerId = sellerId;
            LikerId = likerId;
        }
    }
}
