using MediatR;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class AddSubscriptionCommandHandler : IRequestHandler<AddSubscriptionCommand, Subscription>
    {
        private readonly string _userId;
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IUserSubscriptionRepository _userSubscriptionRepository;

        public AddSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository, IUserSubscriptionRepository userSubscriptionRepository)
        {
            _subscriptionRepository = subscriptionRepository;
            _userSubscriptionRepository = userSubscriptionRepository;
        }

        public async Task<Subscription> Handle(AddSubscriptionCommand request, CancellationToken cancellationToken)
        {
            if (_userId.Equals(request.RecipientId)) throw new DuplicateValueException("The provided SubscriberId and RecipientId are the same");

            if (request.Type != UserType.Seller) throw new InvalidOperationException("The provided item Id can't subscribe other users");

            var subscribe = new Subscription { SubscriberId = _userId };

            await _subscriptionRepository.CreateAsync(subscribe);
            var userSubscription = new UserSubscription(request.RecipientId, subscribe.Id);
            await _userSubscriptionRepository.CreateAsync(userSubscription);

            return subscribe;
        }
    }
}
