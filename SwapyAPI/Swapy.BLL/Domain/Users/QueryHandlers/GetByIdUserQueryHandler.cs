using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.Common.DTO.Users.Responses;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class GetByIdUserQueryHandler : IRequestHandler<GetByIdUserQuery, UserResponseDTO>
    {
        private readonly UserManager<User> _userManager;

        public GetByIdUserQueryHandler(UserManager<User> userManager) => _userManager = userManager;

        public async Task<UserResponseDTO> Handle(GetByIdUserQuery request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id.Equals(request.UserId));

            return new UserResponseDTO()
            {
                Logo = user.Logo,
                LastName = user.LastName,
                FirstName = user.FirstName,
                LikesCount = user.LikesCount,
                ProductsCount = user.ProductsCount,
                RegistrationDate = user.RegistrationDate,
                SubscriptionsCount = user.SubscriptionsCount,
            };
        }
    }
}
