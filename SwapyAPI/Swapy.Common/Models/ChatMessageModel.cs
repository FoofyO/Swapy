namespace Swapy.Common.Models
{
    public class ChatMessageModel
    {
        public string ChatId { get; set; }
        public string RecepientId { get; set; }
        public string SenderId { get; set; }
        public string Message { get; set; }
        public string Image { get; set; }
        public DateTime DateTime { get; set; }

        public ChatMessageModel(string chatId, string recepientId, string senderId, string message, string image, DateTime dateTime)
        {
            ChatId = chatId;
            RecepientId = recepientId;
            SenderId = senderId;
            Message = message;
            Image = image;
            DateTime = dateTime;
        }
    }
}