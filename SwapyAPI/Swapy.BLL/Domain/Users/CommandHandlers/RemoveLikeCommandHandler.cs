using MediatR;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;
using Swapy.DAL.Repositories;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class RemoveLikeCommandHandler : IRequestHandler<RemoveLikeCommand, Unit>
    {
        private readonly string _userId;
        private readonly ILikeRepository _likeRepository;

        public RemoveLikeCommandHandler(ILikeRepository likeRepository) => _likeRepository = likeRepository;

        public async Task<Unit> Handle(RemoveLikeCommand request, CancellationToken cancellationToken)
        {
            var like = await _likeRepository.GetByIdAsync(request.LikeId);

            if (like.LikerId != _userId) throw new NoAccessException("No access to delete this like");

            await _likeRepository.DeleteAsync(like);

            return Unit.Value;
        }
    }
}
