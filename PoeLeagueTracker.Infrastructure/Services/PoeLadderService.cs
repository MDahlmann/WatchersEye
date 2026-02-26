using PoeLeagueTracker.Application.ServiceInterfaces;
using PoeLeagueTracker.Domain.Accounts;
using PoeLeagueTracker.Domain.Characters;
using PoeLeagueTracker.Domain.Leagues;
using PoeLeagueTracker.Infrastructure.RefitInterfaces;
using System.Net;

namespace PoeLeagueTracker.Infrastructure.Services
{
    public class PoeLadderService(IGggApi gggApi) : IPoeLadderService
    {
        private readonly IGggApi _gggApi = gggApi;

        async Task<League?> IPoeLadderService.GetLeagueAsync(string leagueId)
        {
            var urlEncodedLeagueId = WebUtility.UrlEncode(leagueId);
            var ladderResponse = await _gggApi.GetGggResponseAsync(urlEncodedLeagueId);

            if (ladderResponse.GggLadderEntries == null) return null;

            var league = League.CreateLeague(leagueId);

            var responseAccDict = new Dictionary<string, Account>();

            foreach (var ladderEntry in ladderResponse.GggLadderEntries)
            {
                if (!responseAccDict.TryGetValue(ladderEntry.GggAccount.Name, out var account))
                {
                    account = Account.CreateAccount(ladderEntry.GggAccount.Name);
                    responseAccDict.Add(account.AccountName, account);
                }

                league.Characters.Add(Character.CreateCharacter(
                ladderEntry.GggCharacter.Id,
                ladderEntry.GggCharacter.Name,
                ladderEntry.GggCharacter.Level,
                ladderEntry.GggCharacter.ClassName.ToEnum<ClassName>(),
                ladderEntry.GggCharacter.Experience,
                ladderEntry.Rank,
                ladderEntry.Dead,
                ladderEntry.Retired,
                ladderEntry.GggCharacter.GggDepth?.DefaultDepth ?? null,
                ladderEntry.GggAccount.GggChallenges.Completed,
                leagueId,
                account.AccountName,
                league,
                account));
            }

            return league;
        }
    }
}
