using PoeLeagueTracker.Application.Interfaces;

namespace PoeLeagueTracker.Application.Leagues.UpsertLeague
{
    public class UpsertLeagueHandler : ICommandHandler<UpsertLeagueCommand>
    {
        private readonly ILeagueRepository _ladderRepo;
        private readonly IPoeLadderService _poeLadderService;

        public UpsertLeagueHandler(ILeagueRepository leagueRepo, IPoeLadderService poeLadderService)
        {
            _ladderRepo = leagueRepo;
            _poeLadderService = poeLadderService;
        }

        async Task ICommandHandler<UpsertLeagueCommand>.HandleAsync(UpsertLeagueCommand command)
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
                foreach (var apiAccount in apiLeague.Accounts)
                {
                    var dbAccount = dbLeague
                        .Accounts
                        .FirstOrDefault(a => a.AccountName == apiAccount.AccountName);

                    if (dbAccount == null)
                    {
                        dbLeague.Accounts.Add(apiAccount);
                    }
                    else
                    {
                        dbAccount.UpdateWithoutCharacters(apiAccount.IsTwitchLinked,
                                                          apiAccount.TwitchUsername,
                                                          apiAccount.CompletedChallenges);

                        foreach (var apiCharacter in apiAccount.Characters)
                        {
                            var dbCharacter = dbAccount.Characters
                                .FirstOrDefault(c => c.Id == apiCharacter.Id);

                            if (dbCharacter == null)
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
