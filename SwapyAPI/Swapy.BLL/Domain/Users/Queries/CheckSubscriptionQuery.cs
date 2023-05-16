using MediatR;

namespace Swapy.BLL.Domain.Users.Queries
{
    public class CheckSubscriptionQuery : IRequest<bool>
    {
        public string RecipientId { get; set; }
    }
}
