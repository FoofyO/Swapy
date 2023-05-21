using MediatR;
using Swapy.Common.Enums;

namespace Swapy.BLL.Domain.Users.Commands
{
    public class UpdateUserCommand : IRequest<Unit>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Logo { get; set; }
    }
}
