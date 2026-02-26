using PoeLeagueTracker.Domain.Leagues;

namespace PoeLeagueTracker.Application.ServiceInterfaces
{
    public interface IPoeLadderService
    {
        Task<League?> GetLeagueAsync(string leagueName);
    }
}
