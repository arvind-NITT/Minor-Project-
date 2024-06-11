using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Models.DTOs;
using MatrimonialApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MatrimonialApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly ILogger<MatchController> _logger;
        public MatchController(IMatchService matchService, ILogger<MatchController> logger)
        {
            _matchService = matchService;
            _logger = logger;
        }
        [Authorize]
        [HttpPost("AddTheMatch")]
        [ProducesResponseType(typeof(Match), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Match>> AddTheMatch(MatchInsertDTO matchInsertDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int user1 = int.Parse(userIdClaim.Value);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var newMatch = await _matchService.AddTheMatch(user1, matchInsertDTO);
                return Ok(newMatch);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
        [Authorize]
        [HttpGet("GetAllTheMatch")]
        [ProducesResponseType(typeof(IEnumerable<Match>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<Match>>> GetAllTheMatch()
        {
            try
            {
                var matches = await _matchService.GetAllTheMatch();
                return Ok(matches);
            }
            catch (Exception ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
        [Authorize]
        [HttpGet("GetTheMatchbyuserId/{user2}")]
        [ProducesResponseType(typeof(Match), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Match>> GetTheMatchbyuserId(int user2)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int user1 = int.Parse(userIdClaim.Value);
            try
            {
                var match = await _matchService.GetTheMatchbyuserId(user1, user2);
                return Ok(match);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }
        [Authorize]
        [HttpDelete("RemoveTheMatch/{id}")]
        [ProducesResponseType(typeof(Match), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Match>> RemoveTheMatch(int id)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            try
            {
                var match = await _matchService.RemoveTheMatch(userId,id);
                return Ok(match);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new ErrorModel(400, ex.Message));
            }
            catch (Exception ex)
            {
                return NotFound(new ErrorModel(404, ex.Message));
            }
        }
        [Authorize]
        [HttpPut("UpdateMatchStatus")]
        [ProducesResponseType(typeof(Match), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Match>> UpdateMatchStatus(MatchUpdateDTO matchUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                return Unauthorized("User ID not found in token.");
            }

            int userId = int.Parse(userIdClaim.Value);
            try
            {
                var updatedMatch = await _matchService.UpdateMatchStatus(userId, matchUpdateDTO);
                return Ok(updatedMatch);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating match status");
                if (ex.Message == "Match not found.")
                {
                    return NotFound(new ErrorModel(404, ex.Message));
                }
                return BadRequest(new ErrorModel(400, ex.Message));
            }
        }
    }
}
