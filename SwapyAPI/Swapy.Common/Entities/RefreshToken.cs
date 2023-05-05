namespace Swapy.Common.Entities
{
    public class RefreshToken
    {
        public Guid Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }

        public RefreshToken() { }

        public RefreshToken(Guid token, DateTime expiresAt, Guid userId)
        {
            Token = token;
            ExpiresAt = expiresAt;
            UserId = userId;
        }
    }
}
