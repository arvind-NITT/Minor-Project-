using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly MatrimonialContext _context;
        private readonly IRepository<int, Subscription> _subscriptionRepo;
        private readonly IRepository<int, Transaction> _transactionRepository;
        public SubscriptionService(MatrimonialContext context,IRepository<int, Subscription> subscriptionRepo, IRepository<int, Transaction> transactionRepository)
        {
            _context = context;
            _subscriptionRepo = subscriptionRepo;
             _transactionRepository = transactionRepository;
        }

        public async Task<Subscription> AddNewSubscription(int userId, SubscriptionDTO subscriptionDTO)
        {
            if (subscriptionDTO == null)
            {
                throw new ArgumentNullException(nameof(subscriptionDTO), "Subscription cannot be null.");
            }

            try
            {
                var startDate = DateTime.Now;
                var endDate = startDate.AddDays(60);

                var subscription = new Subscription
                {
                    UserId = userId,
                    Type = subscriptionDTO.Type,
                    TransactionId = 0,
                    StartDate = startDate,
                    EndDate = endDate
                };
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

        public async Task<Subscription> UpdateSubscription(int userId, SubscriptionDTO subscriptionDTO)
        {
            if (subscriptionDTO == null)
            {
                throw new ArgumentNullException(nameof(subscriptionDTO), "Subscription cannot be null.");
            }

            try
            {
                var existingSubscription=   await _context.Subscriptions.SingleOrDefaultAsync(u => u.UserId == userId);
                if (existingSubscription == null)
                {
                    throw new Exception("Subscription not found.");
                }
                var transaction = await _transactionRepository.Get(subscriptionDTO.TransactionId);
                if(transaction == null )
                {
                    throw new Exception("Transaction not found.");
                }
                if (transaction.UserId != userId || transaction.IsApproved == true)
                {
                    throw new Exception("Invalid Transaction");
                }
                else
                {
                    transaction.IsApproved = true;
                    await _transactionRepository.Update(transaction);
                }
                var startDate = DateTime.Now;
                var endDate = startDate.AddDays(60);
                // Map the DTO to the Subscription model
                var updatedSubscription = new Subscription
                {
                    SubscriptionId = existingSubscription.SubscriptionId,
                    UserId = userId,
                    Type = subscriptionDTO.Type,
                    TransactionId = subscriptionDTO.TransactionId,
                    StartDate = startDate,
                    EndDate = endDate
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
