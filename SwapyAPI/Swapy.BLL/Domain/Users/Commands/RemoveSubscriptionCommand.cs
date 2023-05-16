using MediatR;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class RemoveSubscriptionCommand : IRequest<Unit>
    {
        public string SubscriptionId { get; set; }
    }
}
