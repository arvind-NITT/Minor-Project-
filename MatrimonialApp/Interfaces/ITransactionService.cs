using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;

namespace MatrimonialApp.Interfaces
{
    public interface ITransactionService
    {
        Task<Transaction> CreateTransaction(int userId, TransactionDTO transactionDTO);
        Task<IEnumerable<Transaction>> GetAllTransactions();
        Task<Transaction> GetTransactionById(int transactionId);
        Task<IEnumerable<Transaction>> GetTransactionsByUserId(int userId);
        Task<Transaction> DeleteTransaction(int transactionId);
        Task<Transaction> UpdateTransactionStatus(int transactionId);

    }
}
