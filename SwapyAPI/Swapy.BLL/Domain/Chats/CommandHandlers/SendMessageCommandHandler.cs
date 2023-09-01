using MediatR;
using Microsoft.AspNetCore.SignalR;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.BLL.Hubs;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.Common.Models;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.CommandHandlers
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Message>
    {
        private readonly IImageService _imageService;
        private readonly IChatRepository _chatRepository;
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IMessageRepository _messageRepository;

        public SendMessageCommandHandler(IImageService imageService, IChatRepository chatRepository, IHubContext<ChatHub> chatHubContext, IMessageRepository messageRepository)
        {
            _imageService = imageService;
            _chatRepository = chatRepository;
            _chatHubContext = chatHubContext;
            _messageRepository = messageRepository;
        }

        public async Task<Message> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            string? imagePath = null;

            if (request.Image != null) imagePath = await _imageService.UploadChatImagesAsync(request.Image);

            var message = new Message(request.Text, imagePath, request.ChatId, request.UserId);

            await _messageRepository.CreateAsync(message);
            
            var recepientId = await _chatRepository.GetChatRecepientIdAsync(request.ChatId, request.UserId);

            var sender = ChatHub.GetConnectedClients().FirstOrDefault(c => c.UserId.Equals(message.SenderId));
            var recepient = ChatHub.GetConnectedClients().FirstOrDefault(c => c.UserId.Equals(recepientId));

            var model = new ChatMessageModel(message.ChatId, recepientId, message.SenderId, message.Text, message.Image, message.DateTime);
            await _chatHubContext.Clients.Client(sender.ConnectionId).SendAsync("ReceiveMessage", model);
            await _chatHubContext.Clients.Client(recepient.ConnectionId).SendAsync("ReceiveMessage", model);

            return message;
        }
    }
}