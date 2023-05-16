using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Chats.Commands
{
    public class CreateChatCommand : IRequest<Chat>
    {
        public string ProductId { get; set; }
    }
}