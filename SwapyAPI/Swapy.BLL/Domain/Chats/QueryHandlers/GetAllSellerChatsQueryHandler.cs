using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.DTO.Chats.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetAllSellerChatsQueryHandler : IRequestHandler<GetAllSellerChatsQuery, ChatsResponseDTO>
    {
        private readonly IChatRepository _chatRepository;
         
        public GetAllSellerChatsQueryHandler(IChatRepository chatRepository) => _chatRepository = chatRepository;

        public async Task<ChatsResponseDTO> Handle(GetAllSellerChatsQuery request, CancellationToken cancellationToken)
        {
            var chats = (await _chatRepository.GetAllSellerChatsAsync(request.UserId)).Select(x => new ChatResponseDTO()
            {
                ChatId = x.Id,
                Logo = x.Buyer.Logo,
                Title = $"{x.Buyer.FirstName} {x.Buyer.LastName}",
                LastMessage = x.Messages.FirstOrDefault()?.Text,
                Image = x.Product.Images.FirstOrDefault()?.Image == null ? "default-product-image.png" : x.Product.Images.FirstOrDefault()?.Image,
                LastMessageDateTime = x.Messages.FirstOrDefault()?.DateTime
            });

            return new ChatsResponseDTO(chats, chats.Count());
        }
    }
}