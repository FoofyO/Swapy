using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.CQRS.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using System.Security.Authentication;

namespace Swapy.BLL.CQRS.CommandHandlers
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task<Unit> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailOrPhone);
            
            if (user == null)
            {
                user = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.EmailOrPhone);
                if (user == null) throw new Exception("Invalid email or password.");
            }
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded) throw new AuthenticationException("Invalid email or password.");
            
            var token = await _tokenService.GenerateToken(user);

            return Unit.Value;
        }
    }
}