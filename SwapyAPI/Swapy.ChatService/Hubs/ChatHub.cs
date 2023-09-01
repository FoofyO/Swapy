using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace Swapy.ChatService.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string recipientId, string message)
        {
            var senderId = Context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            await Clients.User(recipientId).SendAsync("ReceiveMessage", senderId, message);

            //await _chatService.SaveMessage(senderId, recipientId, message);
        }
    }
}
