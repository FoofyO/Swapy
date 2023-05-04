using MediatR;
using Swapy.BLL.CQRS.Commands;

namespace Swapy.BLL.CQRS.CommandHandlers
{
    public class RegistrationCommandHandler : IRequestHandler<RegistrationCommand, Unit>
    {
        public Task<Unit> Handle(RegistrationCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}