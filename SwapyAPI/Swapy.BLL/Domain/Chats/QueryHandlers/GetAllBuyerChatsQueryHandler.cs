using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetAllBuyerChatsQueryHandler : IRequestHandler<GetAllBuyerChatsQuery, IEnumerable<Chat>>
    {
        private readonly IChatRepository _chatRepository;
         
        public GetAllBuyerChatsQueryHandler(IChatRepository chatRepository) => _chatRepository = chatRepository;

        public async Task<IEnumerable<Chat>> Handle(GetAllBuyerChatsQuery request, CancellationToken cancellationToken)
        {
            return await _chatRepository.GetAllBuyerChatsAsync(request.UserId);
        }
    }
}