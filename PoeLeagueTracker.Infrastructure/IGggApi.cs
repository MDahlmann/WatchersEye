using PoeLeagueTracker.Infrastructure.ApiModels;
using Refit;

namespace PoeLeagueTracker.Infrastructure
{
    public interface IGggApi
    {
        [Get("/ladders/{leagueId}?limit={limit}")]
        Task<GggLadderResponse> GetLadderAsync(string leagueId, int limit);
    }
}
