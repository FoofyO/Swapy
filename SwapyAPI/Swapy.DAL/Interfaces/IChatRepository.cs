﻿using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IChatRepository : IRepository<Chat>
    {
        Task<IEnumerable<Chat>> GetAllSellerChatsAsync(string userId);
        Task<IEnumerable<Chat>> GetAllBuyerChatsAsync(string userId);
        Task<Chat> GetByIdDetailAsync(string id);
    }
}
