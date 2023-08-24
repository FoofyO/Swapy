using MediatR;
using Swapy.BLL.Domain.Chats.Queries;
using Swapy.Common.DTO.Chats.Responses;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.QueryHandlers
{
    public class GetDetailChatByProductIdQueryHandler : IRequestHandler<GetDetailChatByProductIdQuery, DetailChatResponseDTO>
    {
        private readonly IChatRepository _chatRepository;

        public GetDetailChatByProductIdQueryHandler(IChatRepository chatRepository) => _chatRepository = chatRepository;

        public async Task<DetailChatResponseDTO> Handle(GetDetailChatByProductIdQuery request, CancellationToken cancellationToken)
        {
            var chat = await _chatRepository.GetByIdDetailByProductIdAsync(request.ProductId);

            return new DetailChatResponseDTO()
            {
                ChatId = chat.Id,
                Messages = chat.Messages.Select(x => new MessageResponseDTO()
                {
                    Id = x.Id,
                    Text = x.Text,
                    Image = x.Image,
                    ChatId = x.ChatId,
                    DateTime = x.DateTime,
                    SenderId = x.SenderId,
                    SenderLogo = x.Sender.Logo
                }),
                Title = chat.Product.Title,
                Image = chat.Product.Images.FirstOrDefault().Image
            };
        }
    }
}
