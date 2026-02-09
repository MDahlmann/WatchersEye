using PoeLeagueTracker.Domain.Leagues;

namespace PoeLeagueTracker.Application.Interfaces
{
    public interface IPoeLadderService
    {
        Task<League?> GetLeagueAsync(string leagueName);
    }
}
