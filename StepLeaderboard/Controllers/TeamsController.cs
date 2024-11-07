using Microsoft.AspNetCore.Mvc;
using StepLeaderboard.Services;

namespace StepLeaderboard.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : ControllerBase
    {
        /// <summary>
        /// Create a new team
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult CreateTeam([FromBody] TeamRequest request)
        {
            var team = TeamService.CreateTeam(request.Name);
            return CreatedAtAction(nameof(GetTeam), new { teamId = team.TeamId }, team);
        }

        /// <summary>
        /// Get all teams
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllTeams()
        {
            var teams = TeamService.GetAllTeams();
            return Ok(teams);
        }

        /// <summary>
        /// Get a specific team
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet("{teamId}")]
        public IActionResult GetTeam(string teamId)
        {
            var team = TeamService.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team);
        }

        /// <summary>
        /// Delete a team
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpDelete("{teamId}")]
        public IActionResult DeleteTeam(string teamId)
        {
            var result = TeamService.DeleteTeam(teamId);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Add a counter to a team
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{teamId}/counters")]
        public IActionResult AddCounter(string teamId, [FromBody] CounterRequest request)
        {
            var counter = TeamService.AddCounterToTeam(teamId, request.EmployeeName);
            if (counter == null)
            {
                return NotFound();
            }
            return CreatedAtAction(nameof(GetCounters), new { teamId, counterId = counter.CounterId }, counter);
        }

        /// <summary>
        /// Get all counters for a team
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet("{teamId}/counters")]
        public IActionResult GetCounters(string teamId)
        {
            var team = TeamService.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(team.Counters);
        }

        /// <summary>
        /// Delete a counter from a team
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="counterId"></param>
        /// <returns></returns>
        [HttpDelete("{teamId}/counters/{counterId}")]
        public IActionResult DeleteCounter(string teamId, string counterId)
        {
            var result = TeamService.DeleteCounterFromTeam(teamId, counterId);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        /// <summary>
        /// Increment steps for a counter
        /// </summary>
        /// <param name="teamId"></param>
        /// <param name="counterId"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("{teamId}/counters/{counterId}/steps")]
        public IActionResult IncrementSteps(string teamId, string counterId, [FromBody] StepRequest request)
        {
            var result = TeamService.IncrementCounterSteps(teamId, counterId, request.Increment);
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }

        /// <summary>
        /// Get total steps of a team
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet("{teamId}/steps")]
        public IActionResult GetTeamSteps(string teamId)
        {
            var team = TeamService.GetTeamById(teamId);
            if (team == null)
            {
                return NotFound();
            }
            return Ok(new { TeamId = team.TeamId, TotalSteps = team.GetTotalSteps() });
        }
    }

    /// Request models for POST data
    public class TeamRequest
    {
        /// <summary>
        /// Team Name
        /// </summary>
        public required string Name { get; set; }
    }

    /// <summary>
    /// Add counter
    /// </summary>
    public class CounterRequest
    {
        /// <summary>
        /// Employee name
        /// </summary>
        public required string EmployeeName { get; set; }
    }

    /// <summary>
    /// Increase counter
    /// </summary>
    public class StepRequest
    {
        /// <summary>
        /// Increase counter
        /// </summary>
        public int Increment { get; set; }
    }
}
