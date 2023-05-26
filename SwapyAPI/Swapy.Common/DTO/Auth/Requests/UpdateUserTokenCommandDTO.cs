namespace Swapy.Common.DTO.Auth.Requests
{
    public class UpdateUserTokenCommandDTO
    {
        public string userid { get; set; }
        public string oldaccesstoken { get; set; }
        public string oldrefreshtoken { get; set; }
    }
}
