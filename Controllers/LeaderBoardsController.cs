using System.Net;
using BrainLadder.Services.LeaderBoards.Models;
using BrainLadder.Services.LeaderBoards;
using Microsoft.AspNetCore.Mvc;

namespace BrainLadder.Controllers;

[Route("api/leaderboard")]
public class LeaderBoardsController : Controller
{
    private readonly ILeaderBoardService _service;

    public LeaderBoardsController(ILeaderBoardService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get top 20 scores by stat
    /// </summary>
    [HttpGet("get-top")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public ActionResult<IEnumerable<LeaderBoardOut>> GetByStat([FromQuery] string stat)
    {
        var result = _service.GetScoresByStat(stat);
        
        if(result != null)
            return Ok(result);
        
        return NoContent();
    }
    
    
    /// <summary>
    /// Add a Score
    /// </summary>
    [HttpPost("set-score")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public ActionResult Create([FromQuery] LeaderBoardIn model)
    {
        _service.AddScore(model);
        return Ok();
    }
}