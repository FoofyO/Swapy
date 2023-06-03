namespace Swapy.Common.DTO.Chats.Responses
{
    public class DetailChatResponseDTO
    {
        public string ChatId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public IEnumerable<MessageResponseDTO> Messages { get; set; }
    }
}
