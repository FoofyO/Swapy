using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Chats.Queries
{
    public class GetDetailChatQuery : IRequest<Chat>
    {
        public string ChatId { get; set; }
    }
}
