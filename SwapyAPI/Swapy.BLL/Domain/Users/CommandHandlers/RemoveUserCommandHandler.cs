using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class RemoveUserCommandHandler : IRequestHandler<RemoveUserCommand, Unit>
    {
        private readonly UserManager<User> _userManager;

        public RemoveUserCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<Unit> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
        {
            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(request.UserId));
            return Unit.Value;
        }
    }
}
