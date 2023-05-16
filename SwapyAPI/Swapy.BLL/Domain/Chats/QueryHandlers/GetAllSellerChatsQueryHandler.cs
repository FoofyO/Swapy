using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetAllSellerChatsQueryHandler : IRequestHandler<GetAllSellerChatsQuery, IEnumerable<Chat>>
    {
        private readonly string _userId;
        private readonly IChatRepository _chatRepository;
         
        public GetAllSellerChatsQueryHandler(IChatRepository chatRepository) => _chatRepository = chatRepository;

        public async Task<IEnumerable<Chat>> Handle(GetAllSellerChatsQuery request, CancellationToken cancellationToken)
        {
            return await _chatRepository.GetAllSellerChatsAsync(_userId);
        }
    }
}