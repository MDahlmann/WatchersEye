using PoeLeagueTracker.Domain.Accounts;
using PoeLeagueTracker.Domain.Characters;

namespace PoeLeagueTracker.Application
{
    public interface IPoeLadderService
    {
        Task GetResponseData(string leagueId);

        Task<IEnumerable<Account>> GetLadderAccountsAsync();

        Task<IEnumerable<Character>> GetLadderCharactersAsync();
    }
}
