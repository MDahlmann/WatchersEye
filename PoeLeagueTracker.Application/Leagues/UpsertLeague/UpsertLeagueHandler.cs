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
            var freshLeague = _poeLadderService.GetLeagueAsync(command.LeagueName);
            var dbLeague = _ladderRepo.GetLeagueAsync(command.LeagueName);


        }
    }
}
