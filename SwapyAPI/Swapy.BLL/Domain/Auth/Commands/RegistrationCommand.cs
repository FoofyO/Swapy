using MediatR;
using Swapy.Common.DTO;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class RegistrationCommand : IRequest<AuthenticationResponseDTO>
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
    }
}