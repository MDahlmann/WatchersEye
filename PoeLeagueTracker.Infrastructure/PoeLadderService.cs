using PoeLeagueTracker.Application;
using PoeLeagueTracker.Domain.Accounts;
using PoeLeagueTracker.Domain.Characters;
using PoeLeagueTracker.Infrastructure.ApiModels;

namespace PoeLeagueTracker.Infrastructure
{
    public class PoeLadderService(IGggApi gggApi) : IPoeLadderService
    {
        private readonly IGggApi _gggApi = gggApi;
        private GggLadderResponse? _ladderResponse;

        async Task IPoeLadderService.GetResponseData(string leagueId)
        {
            _ladderResponse = await _gggApi.GetGggResponseAsync(leagueId);
        }

        async Task<IEnumerable<Account>> IPoeLadderService.GetLadderAccountsAsync()
        {
            List<Account> accounts = [];

            if (_ladderResponse == null) throw new NullReferenceException();

            foreach (var GggLadderEntry in _ladderResponse.GggLadderEntries)
            {
                if (!accounts.Any(a => a.AccountName == GggLadderEntry.GggAccount.Name))
                {
                    accounts.Add(Account.CreateAccount(
                        GggLadderEntry.GggAccount.Name,
                        GggLadderEntry.GggAccount.IsTwitchLinked,
                        GggLadderEntry.GggAccount.GggTwitch.TwitchUsername,
                        GggLadderEntry.GggAccount.GggChallenges.Completed));
                }
            }

            return accounts;
        }

        async Task<IEnumerable<Character>> IPoeLadderService.GetLadderCharactersAsync()
        {
            List<Character> characters = [];

            if (_ladderResponse == null) throw new NullReferenceException();

            foreach (var GggLadderEntry in _ladderResponse.GggLadderEntries)
            {
                characters.Add(Character.CreateCharacter(
                    GggLadderEntry.GggCharacter.Id,
                    GggLadderEntry.GggCharacter.Name,
                    GggLadderEntry.GggCharacter.Level,
                    GggLadderEntry.GggCharacter.ClassName.ToEnum<ClassName>(),
                    GggLadderEntry.GggCharacter.Experience,
                    GggLadderEntry.GggAccount.Name,
                    GggLadderEntry.Rank,
                    GggLadderEntry.Dead,
                    GggLadderEntry.Retired,
                    GggLadderEntry.IsPublic,
                    GggLadderEntry.GggCharacter.GggDepth.DefaultDepth));
            }

            return characters;
        }
    }
}
