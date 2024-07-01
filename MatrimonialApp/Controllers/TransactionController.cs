using MatrimonialApp.Interfaces;
using MatrimonialApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatrimonialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;

        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        [Authorize]
        [HttpPost("DoTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] TransactionDTO transactionDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userid = int.Parse(userIdClaim.Value);
 
            if (transactionDTO == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var transaction = await _transactionService.CreateTransaction(userid, transactionDTO);

            if (transaction == null)
            {
                return StatusCode(500, "An error occurred while creating the transaction.");
            }

            return Ok(transaction);
        }
        [Authorize]
        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var transactions = await _transactionService.GetAllTransactions();
            return Ok(transactions);
        }
        //[Authorize]
        //[HttpGet("GetTransactionById")]
        //public async Task<IActionResult> GetTransactionById(int transactionId)
        //{
        //    var transaction = await _transactionService.GetTransactionById(transactionId);

        //    if (transaction == null)
        //    {
        //        return NotFound("Transaction not found.");
        //    }

        //    return Ok(transaction);
        //}
        [Authorize]
        [HttpGet("GetTransactionsByUserId")]
        public async Task<IActionResult> GetTransactionsByUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userid = int.Parse(userIdClaim.Value);
            var transactions = await _transactionService.GetTransactionsByUserId(userid);
            return Ok(transactions);
        }

    }
}
