using MediatR;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class RemoveLikeCommand : IRequest<Unit>
    {
        public string LikeId { get; set; }
    }
}
