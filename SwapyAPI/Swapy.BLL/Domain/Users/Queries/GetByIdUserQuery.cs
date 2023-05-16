using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Users.Queries
{
    public class GetByIdUserQuery : IRequest<User>
    {
        public string UserId { get; set; }
    }
}
