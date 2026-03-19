using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Application.Queries.GetLeagueQuery
{
    public record GetLeagueQuery(string leagueName) : IQuery<LeagueDto?>
    {
    }
}
