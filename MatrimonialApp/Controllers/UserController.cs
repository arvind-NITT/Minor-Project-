using MainRequestTrackerAPI.Models.DTOs;
using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;

namespace MatrimonialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        [HttpPost("Login")]
        [ProducesResponseType(typeof(LoginReturnDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status401Unauthorized)]
        public async Task<ActionResult<LoginReturnDTO>> Login(UserLoginDTO userLoginDTO)
        {
            try
            {
                var result = await _userService.Login(userLoginDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError("User is Not Authenticated, Invalid Password");
                return Unauthorized(new ErrorModel(401, ex.Message));
            }
        }
        [HttpPost("Register")]
        [ProducesResponseType(typeof(User), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<User>> Register(UserDTO userDTO)
        {
            try
            {
                User result = await _userService.Register(userDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(501, ex.Message));
            }
        }

        [Authorize]
        [HttpGet("GetMyMatches/{userId}")]
        [ProducesResponseType(typeof(IEnumerable<MatchDetailsDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MatchDetailsDTO>>> GetMyMatches(int userId)
        {
            try
            {
                LoginReturnDTO loginReturnDTO = new LoginReturnDTO();
                loginReturnDTO.UserID = userId;
                var result = await _userService.GetMyMatchs(loginReturnDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }
        [Authorize]
        [HttpPost("FindMyMatch")]
        [ProducesResponseType(typeof(IEnumerable<MatchReturnDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<MatchReturnDTO>>> FindMyMatch(MatchDTO matchDTO)
        {
            try
            {
                var result = await _userService.FindMyMatch(matchDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }

        [Authorize]
        [HttpPost("MakeMyProfile")]
        [ProducesResponseType(typeof(Profile), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Profile>> MakeMyProfile(ProfileDTO profileDTO)
        {
            if (profileDTO == null)
            {
                return BadRequest(new ErrorModel(400, "Profile data is required."));
            }

            try
            {
                var profile = await _userService.MakeMyProfile(profileDTO);
                return Ok(profile);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(500, ex.Message));
            }
        }

    }
}
