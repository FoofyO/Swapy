using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<IEnumerable<Chat>> GetAllSellerChatsAsync(string userId);
        Task<IEnumerable<Chat>> GetAllBuyerChatsAsync(string userId);
        Task<Chat> GetByIdDetailAsync(string id);
        Task<Chat> CheckChatExists(string userId, string productId);
        Task<Chat> GetByIdDetailByProductIdAsync(string productId, string userId);
        Task<string> GetChatRecepientIdAsync(string chatId, string senderId);
    }
}
