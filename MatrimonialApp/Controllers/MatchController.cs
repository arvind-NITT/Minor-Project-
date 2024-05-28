using MatrimonialApp.Interfaces;
using MatrimonialApp.Models;
using MatrimonialApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<Match>> AddTheMatch(Match match)
        {
            try
            {
                var newMatch = await _matchService.AddTheMatch(match);
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
        [HttpGet("GetTheMatchbyuserId/{user1}/{user2}")]
        [ProducesResponseType(typeof(Match), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ErrorModel), StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Match>> GetTheMatchbyuserId(int user1, int user2)
        {
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
            try
            {
                var match = await _matchService.RemoveTheMatch(id);
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
        public async Task<ActionResult<Match>> UpdateMatchStatus(Match match)
        {
            try
            {
                var updatedMatch = await _matchService.UpdateMatchStatus(match);
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
