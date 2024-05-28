using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using Microsoft.EntityFrameworkCore;

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
            return (await _context.Subscriptions.SingleOrDefaultAsync(u => u.SubscriptionID == key)) ?? throw new Exception("No Subscription with the given ID");
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
            var Subscription = await Get(item.SubscriptionID);
            if (Subscription != null)
            {
                _context.Update(item);
                await _context.SaveChangesAsync();
                return Subscription;
            }
            throw new Exception("No Subscription with the given ID");
        }
    }
}
