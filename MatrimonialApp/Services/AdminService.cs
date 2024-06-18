using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MatrimonialApp.Models;
using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Repositories;
using MatrimonialApp.Models.DTOs;

namespace MatrimonialApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly MatrimonialContext _context;
        private readonly IRepository<int, PricingPlan> _pricingPlanRepo;
        //private readonly IRepository<int, Subscription> _subscriptionRepo;
        public AdminService(MatrimonialContext context, IRepository<int, PricingPlan> pricingPlanRepo)
        {
            _context = context;
            _pricingPlanRepo = pricingPlanRepo;
            //_subscriptionRepo = subscriptionRepo;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FindAsync(userId);
        }



        public async Task DeleteUserAsync(int userId)
        {
            //using (var transaction = await _context.Database.BeginTransactionAsync())
            //{
                try
                {
                    var user = await _context.Users.FindAsync(userId);
                    if (user == null)
                    {
                        throw new Exception("User not found.");
                    }

                    var profile = await _context.Profiles.FirstOrDefaultAsync(p => p.UserID == userId);
                    var subscriptions = await _context.Subscriptions.Where(s => s.UserId == userId).ToListAsync();
                    var matches = await _context.Matchs.Where(m => m.UserID1 == userId || m.UserID2 == userId).ToListAsync();

                    if (profile != null)
                    {
                        _context.Profiles.Remove(profile);
                    }

                    if (subscriptions.Any())
                    {
                        _context.Subscriptions.RemoveRange(subscriptions);
                    }

                    if (matches.Any())
                    {
                        _context.Matchs.RemoveRange(matches);
                    }

                     _context.Users.Remove(user);

                    await _context.SaveChangesAsync();
                    //await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    //await transaction.RollbackAsync();
                    throw;
                }
            
        }

        public async Task<IEnumerable<Profile>> GetAllProfilesAsync()
        {
            return await _context.Profiles.ToListAsync();
        }

        public async Task<Profile> GetProfileByIdAsync(int profileId)
        {
            return await _context.Profiles.FindAsync(profileId);
        }

        public async Task UpdateProfileAsync(Profile profile)
        {
            _context.Profiles.Update(profile);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProfileAsync(int profileId)
        {
            var profile = await _context.Profiles.FindAsync(profileId);
            if (profile != null)
            {
                _context.Profiles.Remove(profile);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Subscription>> GetAllSubscriptionsAsync()
        {
            return await _context.Subscriptions.ToListAsync();
        }

        public async Task<Subscription> GetSubscriptionByIdAsync(int subscriptionId)
        {
            return await _context.Subscriptions.FindAsync(subscriptionId);
        }

        public async Task UpdateSubscriptionAsync(Subscription subscription)
        {
            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSubscriptionAsync(int subscriptionId)
        {
            var subscription = await _context.Subscriptions.FindAsync(subscriptionId);
            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<int> GetUserCountRegisteredTodayAsync()
        {
            var today = DateTime.UtcNow.Date;
            return await _context.UserDetails
                                 .Where(u => u.RegistrationDate.Date == today)
                                 .CountAsync();
        }
        public async Task<IEnumerable<PricingPlan>> GetAllPricingPlansAsync()
        {
            return await _pricingPlanRepo.Get();
        }

        public async Task<PricingPlan> GetPricingPlanByIdAsync(int pricingPlanId)
        {
            return await _pricingPlanRepo.Get(pricingPlanId);
        }

        public async Task AddPricingPlanAsync(PricingPlan pricingPlan)
        {
            await _pricingPlanRepo.Add(pricingPlan);
        }

        public async Task UpdatePricingPlanAsync(PriceplansupdateDTO priceplansupdateDTO)
        {
            var pricedetails = await _context.PricingPlans.FirstOrDefaultAsync(pricedetails => pricedetails.Type == priceplansupdateDTO.Type);
            if (pricedetails == null) { throw new Exception("Not Found"); }
            pricedetails.Price = priceplansupdateDTO.Price;
            pricedetails.Description = priceplansupdateDTO.Description;
            await _pricingPlanRepo.Update(pricedetails);
        }

        public async Task DeletePricingPlanAsync(int pricingPlanId)
        {
            await _pricingPlanRepo.Delete(pricingPlanId);
        }

        public async Task<decimal> GetTotalEarnings()
        {
            var totalEarnings = await _context.Transactions.SumAsync(t=> t.Amount);
            return totalEarnings;
        }
        public async Task<(int BasicPlanUsers, int PremiumPlanUsers)> GetSubscriptionCountsAsync()
        {
            var basicPlanUsers = await _context.Subscriptions
            .Where(s => s.Type == SubscriptionType.Basic)
            .CountAsync();

            var premiumPlanUsers = await _context.Subscriptions
                .Where(s => s.Type == SubscriptionType.Premium)
                .CountAsync();

            return (basicPlanUsers, premiumPlanUsers);
        }

    }
}
