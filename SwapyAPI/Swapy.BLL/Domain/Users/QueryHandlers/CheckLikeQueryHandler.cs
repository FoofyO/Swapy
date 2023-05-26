using MediatR;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class CheckLikeQueryHandler : IRequestHandler<CheckLikeQuery, bool>
    {
        private readonly IUserLikeRepository _userLikeRepository;

        public CheckLikeQueryHandler(IUserLikeRepository userLikeRepository) => _userLikeRepository = userLikeRepository;

        public async Task<bool> Handle(CheckLikeQuery request, CancellationToken cancellationToken)
        {
            if(request.UserId.Equals(request.RecipientId)) throw new DuplicateValueException("The provided LikerId and RecepientId are the same");
            return await _userLikeRepository.CheckUserLikeAsync(request.UserId, request.RecipientId);
        }
    }
}
