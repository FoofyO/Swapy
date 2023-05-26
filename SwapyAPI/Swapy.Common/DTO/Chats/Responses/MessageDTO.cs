namespace Swapy.Common.DTO.Chats.Responses
{
    public class MessageDTO
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime DateTime { get; set; }
        public string ChatId { get; set; }
        public string SenderId { get; set; }
    }
}
