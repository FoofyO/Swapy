using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.DTO.Chats.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetAllBuyerChatsQueryHandler : IRequestHandler<GetAllBuyerChatsQuery, ChatsResponseDTO>
    {
        private readonly IChatRepository _chatRepository;
         
        public GetAllBuyerChatsQueryHandler(IChatRepository chatRepository) => _chatRepository = chatRepository;

        public async Task<ChatsResponseDTO> Handle(GetAllBuyerChatsQuery request, CancellationToken cancellationToken)
        {
            var chats = (await _chatRepository.GetAllBuyerChatsAsync(request.UserId)).Select(x => new ChatResponseDTO()
            {
                ChatId = x.Id,
                Logo = x.Product.User.Logo,
                Title = x.Product.Title,
                LastMessage = x.Messages.FirstOrDefault().Text,
                Image = x.Product.Images.FirstOrDefault().Image,
                LastMessageDateTime = x.Messages.FirstOrDefault().DateTime
            });

            return new ChatsResponseDTO(chats, chats.Count());
        }
    }
}