using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetDetailChatQueryHandler : IRequestHandler<GetDetailChatQuery, Chat>
    {
        private readonly IChatRepository _chatRepository;

        public GetDetailChatQueryHandler(IChatRepository chatRepository) => _chatRepository = chatRepository;

        public async Task<Chat> Handle(GetDetailChatQuery request, CancellationToken cancellationToken)
        {
            return await _chatRepository.GetByIdDetailAsync(request.ChatId);
        }
    }
}