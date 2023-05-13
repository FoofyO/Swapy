using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class CheckPhoneCommandHandler : IRequestHandler<PhoneNumberCommand, bool>
    {
        private readonly UserManager<User> _userManager;

        public CheckPhoneCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<bool> Handle(PhoneNumberCommand request, CancellationToken cancellationToken)
        {
            var result = await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == request.PhoneNumber);
            if (result == null) return false;
            return true;
        }
    }
}
