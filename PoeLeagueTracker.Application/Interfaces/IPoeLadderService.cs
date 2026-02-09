using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Application.Interfaces
{
    public interface IPoeLadderService
    {
        Task<IEnumerable<Account>> GetLadderDataAsync(string leagueId);
    }
}
