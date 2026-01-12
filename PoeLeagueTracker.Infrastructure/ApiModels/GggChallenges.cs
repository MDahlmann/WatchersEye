using System.Text.Json.Serialization;

namespace PoeLeagueTracker.Infrastructure.ApiModels
{
    public class GggChallenges
    {
        [JsonPropertyName("set")]
        public string Set { get; set; }

        [JsonPropertyName("completed")]
        public int Completed { get; set; }

        [JsonPropertyName("max")]
        public int MaxChallenges { get; set; }

        public GggChallenges(string set, int completed, int maxChallenges)
        {
            Set = set;
            Completed = completed;
            MaxChallenges = maxChallenges;
        }
    }
}
