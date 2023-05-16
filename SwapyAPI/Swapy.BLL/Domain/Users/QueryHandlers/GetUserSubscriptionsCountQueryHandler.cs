using MediatR;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class GetUserSubscriptionsCountQueryHandler : IRequestHandler<GetUserSubscriptionsCountQuery, int>
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public GetUserSubscriptionsCountQueryHandler(IUserSubscriptionRepository userSubscriptionRepository) => _userSubscriptionRepository = userSubscriptionRepository;

        public async Task<int> Handle(GetUserSubscriptionsCountQuery request, CancellationToken cancellationToken)
        {
            return await _userSubscriptionRepository.GetCountByUserIdAsync(request.UserId);
        }
    }
}
