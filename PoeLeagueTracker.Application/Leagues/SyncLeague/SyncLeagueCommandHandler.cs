using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Domain.Accounts;
using PoeLeagueTracker.Domain.Characters;
using PoeLeagueTracker.Domain.Leagues;

namespace PoeLeagueTracker.Application.Leagues.SyncLeague
{
    public class SyncLeagueCommandHandler : ICommandHandler<SyncLeagueCommand>
    {
        private readonly ILeagueRepository _ladderRepo;
        private readonly IPoeLadderService _poeLadderService;
        private readonly IAccountRepository _accountRepository;

        public SyncLeagueCommandHandler(ILeagueRepository leagueRepo, IPoeLadderService poeLadderService, IAccountRepository accountRepository)
        {
            _ladderRepo = leagueRepo;
            _poeLadderService = poeLadderService;
            _accountRepository = accountRepository;
        }

        async Task ICommandHandler<SyncLeagueCommand>.HandleAsync(SyncLeagueCommand command)
        {
            var apiLeague = await _poeLadderService.GetLeagueAsync(command.LeagueName)
                ?? throw new ArgumentException($"{command.LeagueName} doesn't exist.");

            var apiAccountNames = apiLeague.Characters.Select(c => c.AccountName)
                                                      .Distinct()
                                                      .ToList();

            var dbLeague = await _ladderRepo.GetLeagueTrackedAsync(command.LeagueName);
            var dbAccounts = await _accountRepository.GetAccountsByNameAsync(apiAccountNames);
            var dbAccDict = dbAccounts.ToDictionary(a => a.AccountName);

            if (dbLeague == null)
            {
                dbLeague = League.CreateLeague(apiLeague.LeagueName);
                await _ladderRepo.AddLeagueAsync(dbLeague);
            }

            var dbCharDict = dbLeague.Characters.ToDictionary(c => c.Id);

            foreach (var character in apiLeague.Characters)
            {
                if (!dbAccDict.TryGetValue(character.AccountName, out var account))
                {
                    account = Account.CreateAccount(character.AccountName);
                    dbAccDict.Add(account.AccountName, account);
                }

                if (!dbCharDict.TryGetValue(character.Id, out var dbCharacter))
                {
                    dbLeague.Characters.Add(Character.CreateCharacter(
                        character.Id,
                        character.Name,
                        character.Level,
                        character.ClassName,
                        character.Experience,
                        character.Rank,
                        character.Dead,
                        character.Retired,
                        character.Depth,
                        character.Challenges,
                        character.LeagueName,
                        character.AccountName,
                        dbLeague,
                        account
                        ));
                }
                else
                {
                    dbCharacter.Update(
                        character.Name,
                        character.Level,
                        character.ClassName,
                        character.Experience,
                        character.Rank,
                        character.Dead,
                        character.Retired,
                        character.Depth,
                        character.Challenges);
                }
            }

            await _ladderRepo.SaveChangesAsync();
        }
    }
}
