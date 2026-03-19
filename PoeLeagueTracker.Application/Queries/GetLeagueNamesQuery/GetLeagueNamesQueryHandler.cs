using PoeLeagueTracker.Application.RepositoryInterfaces;

namespace PoeLeagueTracker.Application.Queries.GetLeagueNamesQuery
{
    public class GetLeagueNamesQueryHandler : IQueryHandler<GetLeagueNamesQuery, List<string>?>
    {
        private readonly ILeagueRepository _repository;

        public GetLeagueNamesQueryHandler(ILeagueRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<string>?> HandleAsync(GetLeagueNamesQuery query)
        {
            return await _repository.GetLeagueNamesAsync();
        }
    }
}
