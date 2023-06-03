namespace Swapy.Common.DTO.Users.Requests
{
    public class UpdateUserCommandDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Logo { get; set; }
    }
}
