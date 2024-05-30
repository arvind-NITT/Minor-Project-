using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatrimonialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
            try
            {
                var subscription = new Subscription
                {
                    // Map properties from subscriptionDTO to subscription
                    UserID = subscriptionDTO.UserID,
                    StartDate = subscriptionDTO.StartDate,
                    EndDate = subscriptionDTO.EndDate,
                    SubscriptionType = subscriptionDTO.SubscriptionType
                    //Plan = subscriptionDTO.Plan,
                    //Status = subscriptionDTO.Status
                };

                var result = await _subscriptionService.AddNewSubscription(subscription);
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

        [HttpGet("GetSubscriptionByUserId/{userId}")]
        [ProducesResponseType(typeof(Subscription), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Subscription>> GetSubscriptionByUserId(int userId)
        {
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

        [HttpDelete("RemoveSubscription/{id}")]
        [ProducesResponseType(typeof(Subscription), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Subscription>> RemoveSubscription(int id)
        {
            try
            {
                var result = await _subscriptionService.RemoveTheSubscription(id);
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
            try
            {
              
                var result = await _subscriptionService.UpdateSubscription(subscriptionDTO);
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
