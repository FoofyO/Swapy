using MediatR;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class LogoutCommand : IRequest<Unit>
    {
        public Guid RefreshToken { get; set; }
    }
}
