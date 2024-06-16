using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatrimonialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;
        private readonly ILogger<ProfileController> _logger;

        public ProfileController(IProfileService profileService, ILogger<ProfileController> logger)
        {
            _profileService = profileService;
            _logger = logger;
        }
        [Authorize]
        [HttpGet("GetProfile")]
        [ProducesResponseType(typeof(Profile), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Profile>> GetProfile()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            try
            {
                var profile = await _profileService.GetMyProfile(userId);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error retrieving profile for UserID: {userId}. Error: {ex.Message}");
                return NotFound();
            }
        }
        [Authorize]
        [HttpPost("AddProfile")]
        [ProducesResponseType(typeof(Profile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Profile>> AddProfile(ProfileDTO profile)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);

            try
            {
                var addedProfile = await _profileService.AddMyProfile(userId,profile);
                return Ok(addedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding profile. Error: {ex.Message}");
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
        [Authorize]
        [HttpPut("UpdateProfile")]
        [ProducesResponseType(typeof(Profile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Profile>> UpdateProfile(ProfileDTO profile)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            try
            {
                //if (userId != profile.UserID)
                //{
                //    return BadRequest(new ErrorModel(400, "UserID in request path does not match UserID in profile data."));
                //}

                var updatedProfile = await _profileService.UpdateMyProfile(userId,profile);
                return Ok(updatedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating profile for UserID: {userId}. Error: {ex.Message}");
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
        [Authorize]
        [HttpDelete("DeleteProfile")]
        [ProducesResponseType(typeof(Profile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Profile>> DeleteProfile()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            try
            {
                var deletedProfile = await _profileService.DeleteMyProfile(userId);
                return Ok(deletedProfile);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting profile for UserID: {userId}. Error: {ex.Message}");
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
    }
}
