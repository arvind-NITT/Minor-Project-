using MatrimonialApp.Contexts;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using MatrimonialApp.Repositories;
using MatrimonialApp.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatrimonialUnitTest
{
    public class TransactionServiceTest
    {
        MatrimonialContext matrimonialContext;
        private IRepository<int, Transaction> _TransactionRepo;
        private TransactionService _transactionService;

        public TransactionServiceTest(){ }   
        
        [SetUp]
        public void Setup()
        {

            DbContextOptionsBuilder optionsBuilder = new DbContextOptionsBuilder()
                                                        .UseInMemoryDatabase("dummyDB");
            matrimonialContext = new MatrimonialContext(optionsBuilder.Options);
            _TransactionRepo = new TransactionRepository(matrimonialContext);
            var inMemorySettings = new Dictionary<string, string> {
                {"Jwt:Key", "This is the dummy key which has to be a bit long for the 512. which should be even more longer for the passing"},
                {"Jwt:Issuer", "*"},
                {"Jwt:Audience", "*"}
            };


            IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();
            _transactionService = new TransactionService(_TransactionRepo);
        }

        [Test]
        public async Task CreateTransaction_ShouldReturnTransaction()
        {
            // Arrange
            var userId = 1;
            var transactionDTO = new TransactionDTO
            {
                TransactionType = "UPI",
                UPIID = "test@upi"
            };


            // Act
            var result = await _transactionService.CreateTransaction(userId, transactionDTO);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(userId, result.UserId);
            Assert.AreEqual(299, result.Amount);
            Assert.AreEqual("UPI", result.TransactionType);
            Assert.False(result.IsApproved);
            Assert.AreEqual("test@upi", result.UPIID);
        }
        [Test]
        public async Task GetAllTransactions_ShouldReturnAllTransactions()
        {
            // Arrange
            var userId = 2;

           // // var transactions = new List<Transaction> { new Transaction(), new Transaction() };
            var transactionDTO = new TransactionDTO
            {
                TransactionType = "UPI",
                UPIID = "test@upi"
            };

           await _transactionService.CreateTransaction(userId, transactionDTO);
           
            // Act
            var result = await _transactionService.GetAllTransactions();

            // Assert
            Assert.AreEqual(2, result.Count());
        }

        [Test]
        public async Task GetTransactionById_ShouldReturnTransaction()
        {
            // Arrange
            //var transactionId = 1;
            //var transaction = new Transaction { TransactionId = transactionId };

            // Act
            var result = await _transactionService.GetTransactionById(1);

            // Assert
            Assert.AreEqual(1, result.TransactionId);
            Assert.NotNull(result);
        }
        [Test]
        public async Task GetTransactionsByUserId_ShouldReturnTransactions()
        {
            // Arrange
            var userId = 1;
            // Act
            var result = await _transactionService.GetTransactionsByUserId(userId);

            // Assert
            Assert.AreEqual(1, result.Count());
        }
        [Test]
        public async Task DeleteTransaction_ShouldReturnDeletedTransaction()
        {
            // Arrange
            var userId = 3;

            // // var transactions = new List<Transaction> { new Transaction(), new Transaction() };
            var transactionDTO = new TransactionDTO
            {
                TransactionType = "UPI",
                UPIID = "test@upi"
            };

            var result1 = await _transactionService.CreateTransaction(userId, transactionDTO);



            // Act
            var result = await _transactionService.DeleteTransaction(result1.TransactionId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(result1.TransactionId, result.TransactionId);
        }
        [Test]
        public async Task UpdateTransactionStatus_ShouldReturnUpdatedTransaction()
        {
            // Arrange
            var transactionId = 1;
            // Act
            var result = await _transactionService.UpdateTransactionStatus(transactionId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.IsApproved);
        }
        [Test]
        public async Task UpdateTransactionStatus_InvalidTransaction_ShouldThrowException()
        {
            // Arrange
            var transactionId = 2;
            // Act & Assert
             Assert.ThrowsAsync<InvalidOperationException>(() => _transactionService.UpdateTransactionStatus(transactionId));
        }

    }
}
