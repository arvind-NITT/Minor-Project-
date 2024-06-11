using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MatrimonialApp.Repositories
{
    public class SubscriptionRepository : IRepository<int, Subscription>
    {
        private MatrimonialContext _context;
        public SubscriptionRepository(MatrimonialContext context)
        {
            _context = context;
        }
        public async Task<Subscription> Add(Subscription item)
        {
            _context.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Subscription> Delete(int key)
        {
            var Subscription = await Get(key);
            if (Subscription != null)
            {
                _context.Remove(Subscription);
                await _context.SaveChangesAsync();
                return Subscription;
            }
            throw new Exception("No Subscription with the given ID");
        }

        public async Task<Subscription> Get(int key)
        {
            Console.WriteLine(key);
            return (await _context.Subscriptions.SingleOrDefaultAsync(u => u.UserId == key)) ?? throw new Exception("No Subscription with the given ID");
        }

        public async Task<IEnumerable<Subscription>> Get()
        {
            return (await _context.Subscriptions.ToListAsync());
        }

        public IQueryable<Subscription> Query()
        {
            throw new NotImplementedException();
        }

        public async Task<Subscription> Update(Subscription item)
        {
            Console.WriteLine("Updating");
            try
            {
                var data = await _context.Subscriptions.FindAsync(item.SubscriptionId); // Use context directly
                if (data == null)
                {
                    throw new Exception("Subscription not found.");
                }
                Console.WriteLine("vfbdnsm,");
                // Update properties
                data.TransactionId = item.TransactionId;
                data.Type = item.Type;
                data.StartDate = item.StartDate;
                data.EndDate = item.EndDate;
                _context.Subscriptions.Update(data);
                _context.Entry(data).State = EntityState.Modified; // Mark entity as modified
                await _context.SaveChangesAsync(); // Save changes to database
                return data;
            }
            catch (Exception ex)
            { 
                throw new Exception("No Subscription with the given ID");
            }
        }
    }
}
