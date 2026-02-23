using Microsoft.AspNetCore.Mvc;
using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Application.Leagues.GetLeague;
using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Api.Controllers
{
    // Unused API-controller equivalent of the Minimal API implemented in
    // program.cs. Written to provide a comparison of the two approaches to APIs

    [ApiController]
    [Route("api/[controller]")]
    public class LeagueController : Controller
    {
        private readonly IQueryHandler<GetLeagueQuery, LeagueDto?> _queryHandler;

        public LeagueController(IQueryHandler<GetLeagueQuery, LeagueDto?> queryHandler)
        {
            _queryHandler = queryHandler;
        }

        [HttpGet("{leagueId}")]
        public async Task<IActionResult> GetLeague(string leagueId)
        {
            var league = await _queryHandler.HandleAsync(new GetLeagueQuery(leagueId));

            if (league == null)
            {
                return NotFound();
            }

            return Ok(league);
        }
    }
}
