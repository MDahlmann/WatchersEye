using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Application.Leagues.GetLeague
{
    public record GetLeagueQuery(string leagueName) : IQuery<LeagueDto>
    {

    }
}
