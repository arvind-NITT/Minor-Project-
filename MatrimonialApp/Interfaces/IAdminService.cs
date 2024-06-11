using MatrimonialApp.Models;

namespace MatrimonialApp.Interfaces
{
    public interface IAdminService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int userId);
        //Task ApproveUserProfileAsync(int userId);
        Task DeleteUserAsync(int userId);
        Task<IEnumerable<Profile>> GetAllProfilesAsync();
        Task<Profile> GetProfileByIdAsync(int profileId);
        Task UpdateProfileAsync(Profile profile);
        Task DeleteProfileAsync(int profileId);
        Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync();
        Task<Subscription> GetSubscriptionByIdAsync(int subscriptionId);
        Task UpdateSubscriptionAsync(Subscription subscription);
        Task DeleteSubscriptionAsync(int subscriptionId);
    }
}
