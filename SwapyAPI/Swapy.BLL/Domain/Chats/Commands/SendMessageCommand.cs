using MediatR;
using Swapy.Common.Entities;

namespace Swapy.BLL.Domain.Chats.Commands
{
    public class SendMessageCommand : IRequest<Message>
    { 
        public string Text { get; set; }
        public string Image { get; set; }
        public string ChatId { get; set; }
    }
}