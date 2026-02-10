using PoeLeagueTracker.Application.Interfaces;
using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Application.Leagues.GetLeague
{
    public class GetLeagueQueryHandler : IQueryHandler<GetLeagueQuery, LeagueDto>
    {
        private readonly ILeagueRepository _repository;

        public GetLeagueQueryHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        async Task<LeagueDto> IQueryHandler<GetLeagueQuery, LeagueDto>.HandleAsync(GetLeagueQuery query)
        {
            throw new NotImplementedException();
        }
    }
}
