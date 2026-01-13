using PoeLeagueTracker.Infrastructure.ApiModels;
using Refit;

namespace PoeLeagueTracker.Infrastructure
{
    public interface IGggApi
    {
        [Get("/ladders/{leagueId}?limit=100")]
        Task<GggLadderResponse> GetGggResponseAsync(string leagueId);
    }
}