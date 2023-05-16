using MediatR;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class RemoveSubscriptionCommandHandler : IRequestHandler<RemoveSubscriptionCommand, Unit>
    {
        private readonly string _userId;
        private readonly ISubscriptionRepository _subscriptionRepository;

        public RemoveSubscriptionCommandHandler(ISubscriptionRepository subscriptionRepository) => _subscriptionRepository = subscriptionRepository;

        public async Task<Unit> Handle(RemoveSubscriptionCommand request, CancellationToken cancellationToken)
        {
            var subscription = await _subscriptionRepository.GetByIdAsync(request.SubscriptionId);

            if (subscription.SubscriberId != _userId) throw new NoAccessException("No access to delete this subscription");

            await _subscriptionRepository.DeleteAsync(subscription);

            return Unit.Value;
        }
    }
}
