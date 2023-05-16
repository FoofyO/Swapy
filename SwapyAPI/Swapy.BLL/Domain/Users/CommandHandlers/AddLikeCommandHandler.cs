using MediatR;
using Microsoft.AspNetCore.Identity;
using Swapy.BLL.Domain.Users.Commands;
using Swapy.Common.Entities;
using Swapy.Common.Enums;
using Swapy.Common.Exceptions;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Users.CommandHandlers
{
    public class AddLikeCommandHandler : IRequestHandler<AddLikeCommand, Like>
    {
        private readonly string _userId;
        private readonly ILikeRepository _likeRepository;
        private readonly IUserLikeRepository _userLikeRepository;

        public AddLikeCommandHandler(ILikeRepository likeRepository, IUserLikeRepository userLikeRepository)
        {
            _likeRepository = likeRepository;
            _userLikeRepository = userLikeRepository;
        }

        public async Task<Like> Handle(AddLikeCommand request, CancellationToken cancellationToken)
        {
            if (_userId.Equals(request.RecipientId)) throw new DuplicateValueException("The provided RecipientId and RecepientId are the same");

            if (request.Type != UserType.Seller) throw new InvalidOperationException("The provided item Id can't like other users");
            
            var like = new Like { LikerId = _userId };
            
            await _likeRepository.CreateAsync(like);
            var userLike = new UserLike(request.RecipientId, like.Id);
            await _userLikeRepository.CreateAsync(userLike);

            return like;
        }
    }
}
