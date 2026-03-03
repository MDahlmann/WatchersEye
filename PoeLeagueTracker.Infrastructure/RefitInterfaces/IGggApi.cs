using PoeLeagueTracker.Infrastructure.ApiModels;
using Refit;

namespace PoeLeagueTracker.Infrastructure.RefitInterfaces
{
    public interface IGggApi
    {
        [Get("/ladders/{leagueId}?limit=200")]
        Task<GggLadderResponse> GetGggResponseAsync(string leagueId);
    }
}