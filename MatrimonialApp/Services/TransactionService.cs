using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly MatrimonialContext _context;
        private readonly IRepository<int, Transaction> _transactionRepository;

        public TransactionService(MatrimonialContext context,IRepository<int, Transaction> transactionRepository)
        {
            _context = context;
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> CreateTransaction(int userId, TransactionDTO transactionDTO)
        {
             var pricedetails = await _context.PricingPlans.FirstOrDefaultAsync(p=> p.Type == transactionDTO.Type);
            if (pricedetails == null) { throw new Exception("Invalid Type"); }
            Console.WriteLine(pricedetails.Type);
            var transaction = new Transaction
            {
                UserId = userId,
                Amount = pricedetails.Price,
                TransactionDate = DateTime.Now,
                TransactionType= "Upgrade",
                IsApproved=false,
                UPIID= transactionDTO.UPIID,
            };

            return await _transactionRepository.Add(transaction);
        }

        public async Task<IEnumerable<Transaction>> GetAllTransactions()
        {
            return await _transactionRepository.Get();
        }

        public async Task<Transaction> GetTransactionById(int transactionId)
        {
            return await _transactionRepository.Get(transactionId);
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId)
        {
            return await _transactionRepository.Query()
                .Where(t => t.UserId == userId)
                .ToListAsync();
        }
        public async Task<Transaction> DeleteTransaction(int transactionId)
        {
            return await _transactionRepository.Delete(transactionId);
        }

        public async Task<Transaction> UpdateTransactionStatus(int transactionId)
        {
            var existingtransaction = await _transactionRepository.Get(transactionId);
            if (existingtransaction != null) { 
                existingtransaction.IsApproved = true;
              return  await _transactionRepository.Update(existingtransaction);
            }
            throw new InvalidOperationException("Invalid Transaction");
        }
    }
}
