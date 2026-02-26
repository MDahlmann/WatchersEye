using PoeLeagueTracker.Application.RepositoryInterfaces;
using PoeLeagueTracker.Shared.DTOs;

namespace PoeLeagueTracker.Application.Queries.GetLeagueQuery
{
    public class GetLeagueQueryHandler : IQueryHandler<GetLeagueQuery, LeagueDto?>
    {
        private readonly ILeagueRepository _repository;

        public GetLeagueQueryHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        async Task<LeagueDto?> IQueryHandler<GetLeagueQuery, LeagueDto?>.HandleAsync(GetLeagueQuery query)
        {
            return await _repository.GetLeagueDtoAsync(query.leagueName);
        }
    }
}
