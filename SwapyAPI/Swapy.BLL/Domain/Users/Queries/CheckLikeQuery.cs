using MediatR;

namespace Swapy.BLL.Domain.Users.Queries
{
    public class CheckLikeQuery : IRequest<bool>
    {
        public string RecipientId { get; set; }
    }
}
