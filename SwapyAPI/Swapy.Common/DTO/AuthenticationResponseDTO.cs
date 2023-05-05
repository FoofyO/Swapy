namespace Swapy.Common.DTO
{
    public class AuthenticationResponseDTO
    {
        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AccessToken { get; set; }
        public Guid RefreshToken { get; set; }
    }
}
