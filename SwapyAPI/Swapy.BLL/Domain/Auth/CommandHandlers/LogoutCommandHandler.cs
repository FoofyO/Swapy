using MediatR;
using Swapy.BLL.Domain.Auth.Commands;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Auth.CommandHandlers
{
    public class LogoutCommandHandler : IRequestHandler<LogoutCommand, Unit>
    {
        private readonly RefreshTokenRepository _refreshTokenRepository;

        public LogoutCommandHandler(RefreshTokenRepository refreshTokenRepository) => _refreshTokenRepository = refreshTokenRepository;

        public async Task<Unit> Handle(LogoutCommand request, CancellationToken cancellationToken)
        {
            var token = await _refreshTokenRepository.GetByIdAsync(request.RefreshToken);
            if (token != null) await _refreshTokenRepository.DeleteAsync(token);
            return Unit.Value;
        }
    }
}
