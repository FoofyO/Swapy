namespace Swapy.BLL.Interfaces
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task<bool> SendConfirmationEmailAsync(string email, string callbackUrl);
    }
}
