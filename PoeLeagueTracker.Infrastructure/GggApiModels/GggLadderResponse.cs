using System.Text.Json.Serialization;

namespace PoeLeagueTracker.Infrastructure.ApiModels
{
    public class GggLadderResponse
    {
        [JsonPropertyName("total")]
        public int TotalEntries { get; set; }

        [JsonPropertyName("entries")]
        public List<GggLadderEntry> GggLadderEntries { get; set; }

        public GggLadderResponse(int totalEntries, List<GggLadderEntry> gggLadderEntries)
        {
            TotalEntries = totalEntries;
            GggLadderEntries = gggLadderEntries;
        }
    }
}
