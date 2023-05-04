using MediatR;

namespace Swapy.BLL.CQRS.Commands
{
    public class LoginCommand : IRequest<Unit>
    {
        public string EmailOrPhone { get; set; }
        public string Password { get; set; }
    }
}