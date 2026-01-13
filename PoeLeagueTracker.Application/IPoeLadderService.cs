using PoeLeagueTracker.Domain.Accounts;

namespace PoeLeagueTracker.Application
{
    public interface IPoeLadderService
    {
        Task<IEnumerable<Account>> GetLadderData(string leagueId);
    }
}
