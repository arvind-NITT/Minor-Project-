using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatrimonialApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowAll")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        // User Management
        [Authorize]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _adminService.GetAllUsersAsync();
            return Ok(users);
        }
        [Authorize]
        [HttpGet("GetUserById/{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var user = await _adminService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }
            return Ok(user);
        }
        [Authorize]
        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _adminService.DeleteUserAsync(userId);
            return NoContent();
        }

        // Profile Management
        [Authorize]
        [HttpGet("GetAllProfiles")]
        public async Task<IActionResult> GetAllProfiles()
        {
            var profiles = await _adminService.GetAllProfilesAsync();
            return Ok(profiles);
        }
        [Authorize]
        [HttpGet("GetProfileById/{profileId}")]
        public async Task<IActionResult> GetProfileById(int profileId)
        {
            var profile = await _adminService.GetProfileByIdAsync(profileId);
            if (profile == null)
            {
                return NotFound("Profile not found.");
            }
            return Ok(profile);
        }
        [Authorize]
        [HttpPut("UpdateProfile")]
        public async Task<IActionResult> UpdateProfile([FromBody] Profile profile)
        {
            if (profile == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _adminService.UpdateProfileAsync(profile);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("DeleteProfile/{profileId}")]
        public async Task<IActionResult> DeleteProfile(int profileId)
        {
            await _adminService.DeleteProfileAsync(profileId);
            return NoContent();
        }

        // Subscription Management
        [Authorize]
        [HttpGet("GetAllSubscriptions")]
        public async Task<IActionResult> GetAllSubscriptions()
        {
            var subscriptions = await _adminService.GetAllSubscriptionsAsync();
            return Ok(subscriptions);
        }
        [Authorize]
        [HttpGet("GetSubscriptionById/{subscriptionId}")]
        public async Task<IActionResult> GetSubscriptionById(int subscriptionId)
        {
            var subscription = await _adminService.GetSubscriptionByIdAsync(subscriptionId);
            if (subscription == null)
            {
                return NotFound("Subscription not found.");
            }
            return Ok(subscription);
        }
        [Authorize]
        [HttpPut("UpdateSubscription")]
        public async Task<IActionResult> UpdateSubscription([FromBody] Subscription subscription)
        {
            if (subscription == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await _adminService.UpdateSubscriptionAsync(subscription);
            return NoContent();
        }
        [Authorize]
        [HttpDelete("DeleteSubscription/{subscriptionId}")]
        public async Task<IActionResult> DeleteSubscription(int subscriptionId)
        {
            await _adminService.DeleteSubscriptionAsync(subscriptionId);
            return Ok("Deleted");
        }
        [Authorize]
        [HttpGet("users-registered-today")]
        public async Task<IActionResult> GetUsersRegisteredToday()
        {
            var count = await _adminService.GetUserCountRegisteredTodayAsync();
            return Ok(new { count });
        }
        [Authorize]
        [HttpGet("pricing-plans")]
        public async Task<IActionResult> GetAllPricingPlans()
        {
            var plans = await _adminService.GetAllPricingPlansAsync();
            return Ok(plans);
        }
        [Authorize]
        [HttpGet("pricing-plan/{id}")]
        public async Task<IActionResult> GetPricingPlanById(int id)
        {
            var plan = await _adminService.GetPricingPlanByIdAsync(id);
            if (plan == null)
            {
                return NotFound("Pricing plan not found.");
            }
            return Ok(plan);
        }
        [Authorize]
        [HttpPost("add-pricing-plan")]
        public async Task<IActionResult> AddPricingPlan([FromBody] PricingPlan pricingPlan)
        {
            await _adminService.AddPricingPlanAsync(pricingPlan);
            return Ok("Pricing plan added successfully.");
        }
        [Authorize]
        [HttpPut("update-pricing-plan")]
        public async Task<IActionResult> UpdatePricingPlan(PriceplansupdateDTO priceplansupdateDTO)
        {
            await _adminService.UpdatePricingPlanAsync(priceplansupdateDTO);
            return Ok("Pricing plan updated successfully.");
        }
        [Authorize]
        [HttpDelete("delete-pricing-plan/{id}")]
        public async Task<IActionResult> DeletePricingPlan(int id)
        {
            await _adminService.DeletePricingPlanAsync(id);
            return Ok("Pricing plan deleted successfully.");
        }
        [Authorize]
        [HttpGet("total-earnings")]
        public async Task<IActionResult> GetTotalEarnings()
        {
            var totalEarnings = await _adminService.GetTotalEarnings();
            return Ok(totalEarnings);
        }
        [Authorize]
        [HttpGet("subscription-counts")]
        public async Task<IActionResult> GetSubscriptionCounts()
        {
            var subscriptionCounts = await _adminService.GetSubscriptionCountsAsync();
            Console.WriteLine(subscriptionCounts);
            return Ok(subscriptionCounts.ToTuple());
        }
    }
}
