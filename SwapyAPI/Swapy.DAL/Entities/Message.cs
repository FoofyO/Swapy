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

<<<<<<< HEAD
        public Message(string text, string image, Guid chatId, Guid senderId) : this()
=======
        public Message(string text, string image, Guid chatId, Guid senderId)
>>>>>>> 6f4a051389e9ad7366ae4969384a08f98ef6bfc0
        {
            Text = text;
            Image = image;
            ChatId = chatId;
            SenderId = senderId;
        }
    }
}
