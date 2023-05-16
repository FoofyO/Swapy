using MediatR;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class CheckSubscriptionQueryHandler : IRequestHandler<CheckSubscriptionQuery, bool>
    {
        private readonly string _userId;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public CheckSubscriptionQueryHandler(IUserSubscriptionRepository userSubscriptionRepository) => _userSubscriptionRepository = userSubscriptionRepository;

        public async Task<bool> Handle(CheckSubscriptionQuery request, CancellationToken cancellationToken)
        {
            return await _userSubscriptionRepository.CheckUserSubscriptionAsync(_userId, request.RecipientId);
        }
    }
}
