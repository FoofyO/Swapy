using MediatR;
using Swapy.Common.DTO;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponseDTO>
    {
        public string OldRefreshToken { get; set; }
    }
}
