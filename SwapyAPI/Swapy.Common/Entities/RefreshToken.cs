namespace Swapy.Common.Entities
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public RefreshToken() { }

        public RefreshToken(string token, DateTime expiresAt, string userId)
        {
            Token = token;
            ExpiresAt = expiresAt;
            UserId = userId;
        }
    }
}
