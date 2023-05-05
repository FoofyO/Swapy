using MediatR;
using Swapy.Common.DTO;

namespace Swapy.BLL.Domain.Auth.Commands
{
    public class RefreshTokenCommand : IRequest<AuthenticationResponseDTO>
    {
        public Guid OldRefreshToken { get; set; }
    }
}
