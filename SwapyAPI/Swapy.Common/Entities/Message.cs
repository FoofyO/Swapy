namespace Swapy.Common.Entities
{
    public class Message
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime DateTime { get; set; }
        public string ChatId { get; set; }
        public Chat Chat { get; set; }
        public string SenderId { get; set; }
        public User Sender { get; set; }

        public Message() { }

        public Message(string text, string image, string chatId, string senderId)
        {
            Text = text;
            Image = image;
            ChatId = chatId;
            SenderId = senderId;
        }
    }
}
