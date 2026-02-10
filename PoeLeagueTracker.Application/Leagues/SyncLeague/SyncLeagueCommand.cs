
using PoeLeagueTracker.Application.Interfaces;

namespace PoeLeagueTracker.Application.Leagues.SyncLeague
{
    public record SyncLeagueCommand(string LeagueName) : ICommand
    {

    }
}
