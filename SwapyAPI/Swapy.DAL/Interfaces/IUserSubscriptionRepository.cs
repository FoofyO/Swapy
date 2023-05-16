using Swapy.Common.Entities;

namespace Swapy.DAL.Interfaces
{
    public interface IUserSubscriptionRepository : IRepository<UserSubscription>
    {
        Task<int> GetCountByUserIdAsync(string userId);
        Task<bool> CheckUserSubscriptionAsync(string subscriberId, string recipientId);
    }
}
