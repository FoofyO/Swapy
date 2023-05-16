using MediatR;
using Swapy.BLL.Domain.Users.Queries;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.QueryHandlers
{
    public class GetUserLikesCountQueryHandler : IRequestHandler<GetUserLikesCountQuery, int>
    {
        private readonly IUserLikeRepository _userLikeRepository;

        public GetUserLikesCountQueryHandler(IUserLikeRepository userLikeRepository) => _userLikeRepository = userLikeRepository;

        public async Task<int> Handle(GetUserLikesCountQuery request, CancellationToken cancellationToken)
        {
            return await _userLikeRepository.GetCountByUserIdAsync(request.UserId);
        }
    }
}
