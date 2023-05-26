using MediatR;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.CommandHandlers
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Message>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        public SendMessageCommandHandler(IMessageRepository messageRepository, IChatRepository chatRepository)
        {
            _chatRepository = chatRepository; 
            _messageRepository = messageRepository;
        }
        public async Task<Message> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            await _chatRepository.GetByIdAsync(request.ChatId);
            var message = new Message(request.Text, request.Image, request.UserId, request.ChatId);
            await _messageRepository.CreateAsync(message); 
            return message;
        } 
    }
} 