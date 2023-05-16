using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Exceptions;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, Unit>
    {
        private readonly string _userId;
        private readonly UserManager<User> _userManager;
        
        public UpdateUserCommandHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<Unit> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(_userId);

            if (user == null) throw new NoAccessException("No access to update this user");

            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Logo = request.Logo;

            await _userManager.UpdateAsync(user);

            return Unit.Value;
        }
    }
}