using MediatR;
using Swapy.Common.DTO;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthResponseDTO>
    {
        public Guid OldRefreshToken { get; set; }
    }
}
