using MediatR;
using Swapy.BLL.Domain.Chats.Commands;
using Swapy.BLL.Interfaces;
using Swapy.Common.Entities;
using Swapy.DAL.Interfaces;

namespace Swapy.BLL.Domain.Chats.CommandHandlers
{
    public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand, Message>
    {
        private readonly IImageService _imageService;
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;

        public SendMessageCommandHandler(IImageService imageService, IChatRepository chatRepository, IMessageRepository messageRepository)
        {
            _imageService = imageService;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
        }

        public async Task<Message> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            await _chatRepository.GetByIdAsync(request.ChatId);
            string? imagePath = null;

            if(request.Image != null) imagePath = await _imageService.UploadChatImagesAsync(request.Image);
            
            var message = new Message(request.Text, imagePath, request.ChatId, request.UserId);
            await _messageRepository.CreateAsync(message); 
            
            return message;
        } 
    }
} 