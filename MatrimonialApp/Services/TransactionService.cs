﻿using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace MatrimonialApp.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly IRepository<int, Transaction> _transactionRepository;

        public TransactionService(IRepository<int, Transaction> transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<Transaction> CreateTransaction(int userId, TransactionDTO transactionDTO)
        {
            var transaction = new Transaction
            {
                UserId = userId,
                Amount = 299,
                TransactionDate = DateTime.Now,
                TransactionType= transactionDTO.TransactionType,
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
