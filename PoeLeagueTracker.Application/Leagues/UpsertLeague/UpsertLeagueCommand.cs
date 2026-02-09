
using PoeLeagueTracker.Application.Interfaces;

namespace PoeLeagueTracker.Application.Leagues.UpsertLeague
{
    public record UpsertLeagueCommand(string LeagueName) : ICommand
    {

    }
}
