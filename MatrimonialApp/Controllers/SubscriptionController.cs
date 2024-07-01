using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatrimonialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class SubscriptionController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;
        private readonly ILogger<SubscriptionController> _logger;

        public SubscriptionController(ISubscriptionService subscriptionService, ILogger<SubscriptionController> logger)
        {
            _subscriptionService = subscriptionService;
            _logger = logger;
        }

        [HttpPost("AddSubscription")]
        [ProducesResponseType(typeof(Subscription), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Subscription>> AddSubscription([FromBody] SubscriptionDTO subscriptionDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            
            try
            {
                var subs = await _subscriptionService.GetTheSubscriptionByUserId(userId);
                if (subs != null)
                {
                    throw new Exception("Subscription Already Exists. Please Go for Update");
                }
                var result = await _subscriptionService.AddNewSubscription(userId,subscriptionDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding subscription.");
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }

        [HttpGet("GetAllSubscriptions")]
        [ProducesResponseType(typeof(IEnumerable<Subscription>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<Subscription>>> GetAllSubscriptions()
        {
            try
            {
                var result = await _subscriptionService.GetAllTheSubscription();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subscriptions.");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorModel(500, ex.Message));
            }
        }

        [HttpGet("GetSubscriptionByUserId")]
        [ProducesResponseType(typeof(Subscription), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Subscription>> GetSubscriptionByUserId()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            try
            {
                var result = await _subscriptionService.GetTheSubscriptionByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving subscription.");
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [HttpDelete("RemoveSubscription")]
        [ProducesResponseType(typeof(Subscription), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Subscription>> RemoveSubscription(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            try
            {
                var result = await _subscriptionService.RemoveTheSubscription(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting subscription.");
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [HttpPut("UpdateSubscription")]
        [ProducesResponseType(typeof(Subscription), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Subscription>> UpdateSubscription([FromBody] SubscriptionDTO subscriptionDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            try
            {
              
                var result = await _subscriptionService.UpdateSubscription(userId,subscriptionDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subscription.");
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
    }
}
