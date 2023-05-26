using MediatR;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class CheckSubscriptionQueryHandler : IRequestHandler<CheckSubscriptionQuery, bool>
    {
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public CheckSubscriptionQueryHandler(IUserSubscriptionRepository userSubscriptionRepository) => _userSubscriptionRepository = userSubscriptionRepository;

        public async Task<bool> Handle(CheckSubscriptionQuery request, CancellationToken cancellationToken)
        {
            if (request.UserId.Equals(request.RecipientId)) throw new DuplicateValueException("The provided Subscriber and RecepientId are the same");
            return await _userSubscriptionRepository.CheckUserSubscriptionAsync(request.UserId, request.RecipientId);
        }
    }
}
