using PoeLeagueTracker.Infrastructure.ApiModels;
using PoeLeagueTracker.Infrastructure.RefitInterfaces;
using System.Text.Json;

namespace PoeLeagueTracker.Infrastructure
{
    public class GggApiMock : IGggApi
    {
        public async Task<GggLadderResponse> GetGggResponseAsync(string leagueId)
        {
            string fileName = "GggApiMockData.json";

            using FileStream openStream = File.OpenRead(fileName);

            var response = await JsonSerializer.DeserializeAsync<GggLadderResponse>(openStream);

            return response!;
        }
    }
}