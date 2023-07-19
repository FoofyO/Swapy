using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, Unit>
    {
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;

        public ForgotPasswordCommandHandler(UserManager<User> userManager, IEmailService emailService, IConfiguration configuration)
        {
            _userManager = userManager;
            _emailService = emailService;
            _configuration = configuration;
        }

        public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null) throw new NotFoundException($"User with {request.Email} email not found");

            var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var callbackUrl = new UriBuilder(_configuration["WebUrl"]);
            callbackUrl.Path = "/auth/reset-password";
            callbackUrl.Query = $"userid={user.Id}&token={resetToken}";
            await _emailService.SendForgotPasswordAsync(user.Email, callbackUrl.Uri.ToString());

            return Unit.Value;
        }
    }
}
