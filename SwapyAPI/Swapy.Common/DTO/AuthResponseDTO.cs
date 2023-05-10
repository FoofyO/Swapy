namespace Swapy.Common.DTO
{
    public class AuthResponseDTO
    {
        public string SellerId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
