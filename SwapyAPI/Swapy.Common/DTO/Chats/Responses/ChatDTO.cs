namespace Swapy.Common.DTO.Chats.Responses
{
    public class ChatDTO
    {
        public string Id { get; set; }
        public string ProductId { get; set; }
        public string BuyerId { get; set; }
        public IEnumerable<MessageDTO> Messages { get; set; }
    }
}
