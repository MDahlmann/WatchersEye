using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Domain.Accounts;
using PoeLeagueTracker.Domain.Characters;
using PoeLeagueTracker.Domain.Leagues;

namespace PoeLeagueTracker.Infrastructure
{
    public class PoeLadderService(IGggApi gggApi) : IPoeLadderService
    {
        private readonly IGggApi _gggApi = gggApi;

        async Task<League?> IPoeLadderService.GetLeagueAsync(string leagueId)
        {
            var ladderResponse = await _gggApi.GetGggResponseAsync(leagueId);

            if (ladderResponse.GggLadderEntries == null) return null;

            var ladderEntries = ladderResponse.GggLadderEntries.GroupBy(le => le.GggAccount.Name);

            List<Account> accounts = [];

            foreach (var accountGroup in ladderEntries)
            {
                List<Character> characters = [];

                foreach (var ladderEntry in accountGroup)
                {
                    characters.Add(Character.CreateCharacter(
                    ladderEntry.GggCharacter.Id,
                    ladderEntry.GggCharacter.Name,
                    ladderEntry.GggCharacter.Level,
                    ladderEntry.GggCharacter.ClassName.ToEnum<ClassName>(),
                    ladderEntry.GggCharacter.Experience,
                    ladderEntry.GggAccount.Name,
                    ladderEntry.Rank,
                    ladderEntry.Dead,
                    ladderEntry.Retired,
                    ladderEntry.IsPublic,
                    ladderEntry.GggCharacter.GggDepth?.DefaultDepth ?? null));
                }

                accounts.Add(Account.CreateAccount(
                    accountGroup.Key,
                    characters,
                    accountGroup.First().GggAccount.IsTwitchLinked,
                    accountGroup.First().GggAccount.GggTwitch?.TwitchUsername ?? null,
                    accountGroup.First().GggAccount.GggChallenges.Completed));
            }

            return League.CreateLeague(leagueId, accounts);
        }
    }
}
