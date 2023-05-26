namespace Swapy.Common.DTO.Auth.Requests
{
    public class UserRegistrationCommandDTO
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string phonenumber { get; set; }
        public string password { get; set; }
    }
}
