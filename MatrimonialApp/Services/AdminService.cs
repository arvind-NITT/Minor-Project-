using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MatrimonialApp.Models;
using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;

namespace MatrimonialApp.Services
{
    public class AdminService : IAdminService
    {
        private readonly MatrimonialContext _context;

        public AdminService(MatrimonialContext context)
        {
            _context = context;
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
    }
}
