using Microsoft.Extensions.Configuration;

namespace PoeLeagueTracker.Application.Queries.GetActiveLeagueQuery
{
    public class GetActiveLeagueQueryHandler : IQueryHandler<GetActiveLeagueQuery, string?>
    {
        private readonly IConfiguration _config;

        public GetActiveLeagueQueryHandler(IConfiguration config)
        {
            _config = config;
        }

        public async Task<string?> HandleAsync(GetActiveLeagueQuery query)
        {
            return _config["ActiveLeague"];
        }
    }
}
