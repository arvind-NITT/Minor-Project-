using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;

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
        Task<int> GetUserCountRegisteredTodayAsync();
        Task<IEnumerable<PricingPlan>> GetAllPricingPlansAsync();
        Task<PricingPlan> GetPricingPlanByIdAsync(int pricingPlanId);
        Task AddPricingPlanAsync(PricingPlan pricingPlan);
        Task UpdatePricingPlanAsync(PriceplansupdateDTO priceplansupdateDTO);
        Task DeletePricingPlanAsync(int pricingPlanId);
        Task<decimal> GetTotalEarnings();
        Task<(int BasicPlanUsers, int PremiumPlanUsers)> GetSubscriptionCountsAsync();
    }
}
