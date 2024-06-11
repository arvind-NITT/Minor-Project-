using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Repositories
{
    public class TransactionRepository : IRepository<int, Transaction>
    {
        private readonly MatrimonialContext _context;

        public TransactionRepository(MatrimonialContext context)
        {
            _context = context;
        }

        public async Task<Transaction> Add(Transaction item)
        {
            _context.Transactions.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public IQueryable<Transaction> Query()
        {
            return _context.Transactions.AsQueryable();
        }

        public async Task<Transaction> Delete(int key)
        {
            var transaction = await _context.Transactions.FindAsync(key);
            if (transaction != null)
            {
                _context.Transactions.Remove(transaction);
                await _context.SaveChangesAsync();
            }
            return transaction;
        }

        public async Task<Transaction> Update(Transaction item)
        {
            _context.Transactions.Update(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<Transaction> Get(int key)
        {
            return await _context.Transactions.FindAsync(key);
        }

        public async Task<IEnumerable<Transaction>> Get()
        {
            return await _context.Transactions.ToListAsync();
        }
    }
}
