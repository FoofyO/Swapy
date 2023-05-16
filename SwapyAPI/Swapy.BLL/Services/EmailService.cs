using Microsoft.Extensions.Configuration;
using Swapy.BLL.Interfaces;
using Swapy.Common.Exceptions;
using System.Net.Http.Headers;
using System.Text;

namespace Swapy.BLL.Services
{
    public class EmailService : IEmailService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public EmailService(IConfiguration configuration)
        {
            _configuration= configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri($"https://api.mailgun.net/v3/{_configuration.GetSection("Email:Domain").Value}/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.ASCII.GetBytes($"api:{_configuration.GetSection("Email:ApiKey").Value}")));
        }

        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("from", _configuration.GetSection("Email:Domain").Value),
                new KeyValuePair<string, string>("to", email),
                new KeyValuePair<string, string>("subject", subject),
                new KeyValuePair<string, string>("text", message)
            });

            var response = await _httpClient.PostAsync("messages", content);
            
            if (!response.IsSuccessStatusCode) throw new EmailSendingException("Failed to send email");
        }

        public async Task<bool> SendConfirmationEmailAsync(string email, string callbackUrl)
        {
            string subject = "Registration Confirmation";
            string message = $"Please confirm your registration by following the link: <a href='{callbackUrl}'>Click here</a>";

            await SendEmailAsync(email, subject, message);

            return true;
        }
    }
}
