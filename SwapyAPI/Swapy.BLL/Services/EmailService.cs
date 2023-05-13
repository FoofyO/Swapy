using Swapy.BLL.Interfaces;
using System.Net;
using System.Net.Mail;

namespace Swapy.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly string _fromEmail;

        public EmailService(string smtpServer, int port, string fromEmail, string username, string password)
        {
            _smtpClient = new SmtpClient(smtpServer, port);
            _smtpClient.Credentials = new NetworkCredential(username, password);
            _fromEmail = fromEmail;
        }

        public async Task<bool> SendConfirmationEmailAsync(string email, string callbackUrl)
        {
            string subject = "Registration Confirmation";
            string message = $"Please confirm your registration by following the link: <a href='{callbackUrl}'>Click here</a>";

            await SendEmailAsync(email, subject, message);

            return true;
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var mailMessage = new MailMessage(_fromEmail, email, subject, message);
            mailMessage.IsBodyHtml = true;

            await _smtpClient.SendMailAsync(mailMessage);
        }
    }
}
