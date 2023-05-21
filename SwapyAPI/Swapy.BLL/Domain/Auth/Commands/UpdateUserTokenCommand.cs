using MediatR;
using Swapy.Common.DTO;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class UpdateUserTokenCommand : IRequest<AuthResponseDTO>
    {
        public string UserId { get; set; }
        public string OldAccessToken { get; set; }
        public string OldRefreshToken { get; set; }
    }
}
