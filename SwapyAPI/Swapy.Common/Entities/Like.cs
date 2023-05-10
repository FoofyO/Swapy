namespace Swapy.Common.Entities
{
    public class Like
    {
        public string Id { get; set; }
        public string LikerId { get; set; }
        public User Liker { get; set; }
        public string UserLikeId { get; set; }
        public UserLike UserLike { get; set; }

        public Like() { }

        public Like(string likerId, string userLikeId)
        {
            LikerId = likerId;
            UserLikeId = userLikeId;
        }
    }
}
