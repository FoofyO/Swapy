using MediatR;

namespace Swapy.BLL.Domain.Users.Queries
{
    public class GetUserSubscriptionsCountQuery : IRequest<int>
    {
        public string UserId { get; set; }
    }
}
