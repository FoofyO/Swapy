using MediatR;

namespace Swapy.BLL.CQRS.Commands
{
    public class RegistrationCommand : IRequest<Unit>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}