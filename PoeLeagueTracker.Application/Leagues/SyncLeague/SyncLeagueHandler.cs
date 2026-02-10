using PoeLeagueTracker.Application.Interfaces;

namespace PoeLeagueTracker.Application.Leagues.SyncLeague
{
    public class SyncLeagueHandler : ICommandHandler<SyncLeagueCommand>
    {
        private readonly ILeagueRepository _ladderRepo;
        private readonly IPoeLadderService _poeLadderService;

        public SyncLeagueHandler(ILeagueRepository leagueRepo, IPoeLadderService poeLadderService)
        {
            _ladderRepo = leagueRepo;
            _poeLadderService = poeLadderService;
        }

        async Task ICommandHandler<SyncLeagueCommand>.HandleAsync(SyncLeagueCommand command)
        {
            var apiLeague = await _poeLadderService.GetLeagueAsync(command.LeagueName);
            var dbLeague = await _ladderRepo.GetLeagueAsync(command.LeagueName);

            if (apiLeague == null) throw new ArgumentException($"{command.LeagueName} doesn't exist.");

            if (dbLeague == null)
            {
                await _ladderRepo.AddLeagueAsync(apiLeague);
            }
            else
            {
                var accountDictionary = dbLeague.Accounts.ToDictionary(a => a.AccountName);

                foreach (var apiAccount in apiLeague.Accounts)
                {
                    if (!accountDictionary.TryGetValue(apiAccount.AccountName, out var dbAccount))
                    {
                        dbLeague.Accounts.Add(apiAccount);
                    }
                    else
                    {
                        dbAccount.UpdateWithoutCharacters(apiAccount.IsTwitchLinked,
                                                          apiAccount.TwitchUsername,
                                                          apiAccount.CompletedChallenges);

                        var characterDictionary = dbAccount.Characters.ToDictionary(c => c.Id);

                        foreach (var apiCharacter in apiAccount.Characters)
                        {
                            if (!characterDictionary.TryGetValue(apiCharacter.Id, out var dbCharacter))
                            {
                                dbAccount.Characters.Add(apiCharacter);
                            }
                            else
                            {
                                dbCharacter.Update(apiCharacter.Name,
                                                   apiCharacter.Level,
                                                   apiCharacter.ClassName,
                                                   apiCharacter.Experience,
                                                   apiCharacter.Rank,
                                                   apiCharacter.Dead,
                                                   apiCharacter.Retired,
                                                   apiCharacter.IsPublic,
                                                   apiCharacter.Depth);
                            }
                        }
                    }
                }
            }
            await _ladderRepo.SaveChangesAsync();
        }
    }
}
