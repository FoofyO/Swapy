namespace Swapy.DAL.Entities
{
    public class Message
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public string Image { get; set; }
        public DateTime DateTime { get; set; }
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }
        public Guid SenderId { get; set; }
        public User Sender { get; set; }

        public Message() { }
        public Message(string text, string image, Guid chatId, Guid senderId) : this()
        {
            Text = text;
            Image = image;
            ChatId = chatId;
            SenderId = senderId;
        }
    }
}
