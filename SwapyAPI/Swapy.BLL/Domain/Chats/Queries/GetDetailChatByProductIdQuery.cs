using MediatR;
using Swapy.Common.DTO.Chats.Responses;

namespace Swapy.BLL.Domain.Chats.Queries
{
    public class GetDetailChatByProductIdQuery : IRequest<DetailChatResponseDTO>
    {
        public string ProductId { get; set; }
    }
}
