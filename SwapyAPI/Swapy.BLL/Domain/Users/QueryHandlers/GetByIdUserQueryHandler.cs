using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, User>
    {
        private readonly UserManager<User> _userManager;

        public GetByIdUserQueryHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<User> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            return await _userManager.Users.Include(u => u.LikesRecipient)
                                           .FirstOrDefaultAsync(u => u.Id.Equals(request.UserId));
        }
    }
}
