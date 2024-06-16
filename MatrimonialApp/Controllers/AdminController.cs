using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MatrimonialApp.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
