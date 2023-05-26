using MediatR;
using Swapy.Common.DTO.Auth.Responses;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class ShopRegistrationCommand : IRequest<AuthResponseDTO>
    {
        public string ShopName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }
}
