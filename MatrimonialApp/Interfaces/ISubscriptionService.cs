using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;

namespace MatrimonialApp.Interfaces
{
    public interface ISubscriptionService
    {
        public Task<Subscription> AddNewSubscription(int userId, SubscriptionDTO subscriptionDTO);
        public Task<Subscription> RemoveTheSubscription(int id);
        public Task<Subscription> UpdateSubscription(int userId, SubscriptionDTO subscriptionDTO);
        public Task<Subscription> GetTheSubscriptionByUserId(int user2);
        public Task<IEnumerable<Subscription>> GetAllTheSubscription();
    }
}
