using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;

namespace MatrimonialApp.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly IRepository<int, Subscription> _subscriptionRepo;

        public SubscriptionService(IRepository<int, Subscription> subscriptionRepo)
        {
            _subscriptionRepo = subscriptionRepo;
        }

        public async Task<Subscription> AddNewSubscription(Subscription subscription)
        {
            if (subscription == null)
            {
                throw new ArgumentNullException(nameof(subscription), "Subscription cannot be null.");
            }

            try
            {
                return await _subscriptionRepo.Add(subscription);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding subscription: {ex.Message}", ex);
            }
        }

        public async Task<IEnumerable<Subscription>> GetAllTheSubscription()
        {
            try
            {
                return await _subscriptionRepo.Get();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving subscriptions: {ex.Message}", ex);
            }
        }

        public async Task<Subscription> GetTheSubscriptionByUserId(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("User ID must be a positive integer.", nameof(userId));
            }

            try
            {
                return await _subscriptionRepo.Get(userId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving subscription for user ID {userId}: {ex.Message}", ex);
            }
        }


        public async Task<Subscription> RemoveTheSubscription(int id)
        {
            if (id <= 0)
            {
                throw new ArgumentException("Subscription ID must be a positive integer.", nameof(id));
            }

            try
            {
                var subscription = await _subscriptionRepo.Get(id);
                if (subscription == null)
                {
                    throw new Exception("Subscription not found.");
                }

                return await _subscriptionRepo.Delete(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting subscription with ID {id}: {ex.Message}", ex);
            }
        }

        public async Task<Subscription> UpdateSubscription(SubscriptionDTO subscriptionDTO)
        {
            if (subscriptionDTO == null)
            {
                throw new ArgumentNullException(nameof(subscriptionDTO), "Subscription cannot be null.");
            }

            try
            {
                var existingSubscription = await _subscriptionRepo.Get(subscriptionDTO.UserID);
                if (existingSubscription == null)
                {
                    throw new Exception("Subscription not found.");
                }

                // Map the DTO to the Subscription model
                var updatedSubscription = new Subscription
                {
                    //SubscriptionID = subscriptionDTO.SubscriptionID,
                    SubscriptionID= existingSubscription.SubscriptionID,
                    UserID = subscriptionDTO.UserID,
                    StartDate = subscriptionDTO.StartDate,
                    EndDate = subscriptionDTO.EndDate,
                    SubscriptionType = subscriptionDTO.SubscriptionType
                };

                return await _subscriptionRepo.Update(updatedSubscription);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating subscription: {ex.Message}", ex);
            }
        }

    }
}
