using Swapy.Common.Enums;

namespace Swapy.Common.DTO
{
    public class AuthResponseDTO
    {
        public UserType Type { get; set; }
        public string UserId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
